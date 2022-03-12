using Application.Features.Characters.Requests.Queries;
using Application.MappingProfiles;
using Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(CharacterProfile).Assembly);
            services.AddMediatR(typeof(GetCharacterListRequest).Assembly);

            services.AddScoped<CharacterService, CharacterService>();

            return services;
        }
    }
}