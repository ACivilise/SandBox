using SandBox.DataAccess.Repositories.ML;
using SandBox.DataAccess.Repositories.ML.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using SandBox.DataAccess.Repositories.Weather.Interfaces;
using SandBox.DataAccess.Repositories.Weather;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        /// Ajoute les services à l'injecteur de dépendances
        /// </summary>
        /// <param name="services">L'interface d'enregistrement de l'injecteur
        /// de dépendances</param>
        /// <returns>L'interface d'enregistrement de l'injecteur de dépendances</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IWeatherRepository, WeatherRepository>()
                .AddScoped<IIrisRepository, IrisRepository>();
        }
    }
}
