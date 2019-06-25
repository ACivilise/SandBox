using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace SandBox.DataAccess.DBContext
{
    public class SandBoxDbContextFactory : IDesignTimeDbContextFactory<SandBoxDbContext>
    {
        public SandBoxDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"Settings\\database.json", optional: true)
                .Build();
            string connectionString = configuration.GetConnectionString("SandBox");
            DbContextOptionsBuilder<SandBoxDbContext> builder = new DbContextOptionsBuilder<SandBoxDbContext>();
            builder.UseSqlServer(connectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(SandBoxDbContext).GetTypeInfo().Assembly.GetName().Name));
            return new SandBoxDbContext(builder.Options);
        }
    }
}
