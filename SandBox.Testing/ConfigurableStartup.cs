using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.Testing
{
    public class ConfigurableStartup : Startup
    {
        private readonly Action<IServiceCollection> configureAction;

        public ConfigurableStartup(IConfiguration configuration, Action<IServiceCollection> configureAction)
            : base(configuration) => this.configureAction = configureAction;

        protected override void ConfigureAdditionalServices(IServiceCollection services)
        {
            configureAction(services);
        }

        protected override void ConfigureAuth(IServiceCollection services)
        {
        }
    }
}
