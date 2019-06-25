using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SandBox.DataAccess.DBContext
{
    public static class DbContextHelper
    {
        /// <summary>
        /// Effectue les migrations automatiques des bases de données
        /// </summary>
        /// <param name="app">App builder</param>
        /// <returns><paramref name="app"/></returns>
        public static IServiceProvider MigrateDb(this IServiceProvider services)
        {
            SandBoxDbContext applicationContext = services.GetRequiredService<SandBoxDbContext>();
            if(!applicationContext.Database.IsInMemory())
            {
                var pendingMigrations = applicationContext.Database.GetPendingMigrations().ToList();
                if (pendingMigrations.Count > 0)
                    applicationContext.Database.Migrate();
            }          
            return services;
        }
        /// <summary>
        /// Initialise les données de l'application
        /// </summary>
        /// <param name="app">App builder</param>
        /// <param name="configuration">Configuration</param>
        /// <returns><paramref name="app"/></returns>
        public static IServiceProvider SeedData(this IServiceProvider services)
        {
            SandBoxDbContext applicationContext = services.GetRequiredService<SandBoxDbContext>();
            var seed = new SandBoxSeed(applicationContext, services);
            seed.Run().Wait();
            return services;
        }
    }
}
