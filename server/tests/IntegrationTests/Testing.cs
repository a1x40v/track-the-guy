using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API;
using Application.Contracts.Infrastructure;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Npgsql;
using NUnit.Framework;
using Persistence;
using Respawn;

namespace IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        public static string _currentUserUsername;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var fullPath = Directory.GetCurrentDirectory();
            var binFoldIdx = fullPath.IndexOf("/bin");
            var path = binFoldIdx > 0 ? fullPath.Substring(0, binFoldIdx) : fullPath;

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            // Mocking IWebHostEnvironment service
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.ApplicationName == "API" &&
                w.EnvironmentName == "Development"
            ));

            startup.ConfigureServices(services);

            // Replace service registration for IUserAccessor
            var userAccessorServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(IUserAccessor));

            services.Remove(userAccessorServiceDescriptor);

            services.AddScoped(provider => Mock.Of<IUserAccessor>(x =>
                x.GetUsername() == _currentUserUsername
            ));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();
            }

            _checkpoint = new Checkpoint
            {
                SchemasToInclude = new[]
                {
                    "public"
                },
                DbAdapter = DbAdapter.Postgres
            };

        }

        public static async Task ResetState()
        {
            using (var conn = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();

                await _checkpoint.Reset(conn);
            }

            _currentUserUsername = null;
        }

        public static async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("tester@test.com", "Testuser", "Pa$$w0rd");
        }

        public static async Task<string> RunAsUserAsync(string email, string username, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();

            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = username,
            };

            var result = await userManager.CreateAsync(user, password);

            _currentUserUsername = user.UserName;

            return _currentUserUsername;
        }

        public static async Task<TEntity> FindAsync<TEntity>(Guid id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }
    }
}