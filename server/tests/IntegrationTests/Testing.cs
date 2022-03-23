using System;
using System.IO;
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
using Microsoft.Extensions.Logging;
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
        public static readonly string CurrentUserUsername = "username";

        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

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

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.ApplicationName == "API" &&
                w.EnvironmentName == "Development"
            ));

            startup.ConfigureServices(services);

            services.AddSingleton(Mock.Of<IUserAccessor>(x =>
                x.GetUsername() == CurrentUserUsername
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