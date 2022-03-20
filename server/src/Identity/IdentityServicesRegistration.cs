using System.Text;
using Application.Contracts.Identity;
using Domain;
using Identity.Policies.Handlers;
using Identity.Policies.Requirements;
using Identity.Services;
using Infrastructure.Security.Policies.Handlers;
using Infrastructure.Security.Policies.Requirements;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                    };
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsAdmin", policy =>
                {
                    policy.RequireClaim("IsAdmin", "True");
                });
                opt.AddPolicy("IsCharacterCreator", policy =>
                {
                    policy.AddRequirements(new CharacterCreatorRequirement());
                });
                opt.AddPolicy("IsReviewAuthor", policy =>
                {
                    policy.AddRequirements(new ReviewCreatorRequirement());
                });
            });

            services.AddScoped<TokenService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IAuthorizationHandler, CharacterCreatorRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ReviewCreatorRequirementHandler>();

            return services;
        }
    }
}