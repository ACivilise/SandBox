using AutoMapper;
using SandBox.Services.Services;
using SandBox.Services.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesCollectionExtensions
    {
        /// <summary>
        /// Ajoute les services à l'injecteur de dépendances
        /// </summary>
        /// <param name="services">L'interface d'enregistrement de l'injecteur
        /// de dépendances</param>
        /// <returns>L'interface d'enregistrement de l'injecteur de dépendances</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IGoogleORService, GoogleORService>()
                .AddScoped<IMLService, MLService>();
        }

        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile<DTOsProfile>();
            });

            return services;
        }
    }
}
