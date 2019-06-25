using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace SandBox.DataAccess.DBContext
{
    class SandBoxSeed
    {
        private readonly IServiceProvider _services;
        public SandBoxDbContext Context { get; private set; }
        private readonly ILogger<SandBoxSeed> _logger;

        public SandBoxSeed(SandBoxDbContext context, IServiceProvider services)
        {
            Context = context;
            _services = services;
            _logger = services.GetService<ILogger<SandBoxSeed>>();
        }

        public virtual async Task Run()
        {
            _logger.LogInformation("Début du Seed");
            _logger.LogInformation("Fin du Seed");
        }
    }
}
