using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.Net.Http.Headers;
using SandBox.ActionFilters;
using SandBox.DataAccess.Mapper;
using SandBox.DataAccess.DBContext;
using SandBox.DTOs.DTOs;
using SandBox.Middlewares;
using System;

namespace SandBox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase();
            var dbContextOptionsBuilder = new Action<DbContextOptionsBuilder>(builder =>
            {
                builder.UseInMemoryDatabase(databaseName: "SandBox");
            });
            services
                .AddDbContext<SandBoxDbContext>(dbContextOptionsBuilder, ServiceLifetime.Scoped)
                .AddScoped<ISandBoxDbContext, SandBoxDbContext>();
            ConfigureAuth(services);
            //augmente le nombre de champs max que peut contenir une form, par défaut cette limite est à 1024
            services.Configure<FormOptions>(options => { options.ValueCountLimit = 10240; });
            //On intègre le modèle au démarage du serveur
            services.AddPredictionEnginePool<IrisData, ClusterPrediction>().FromFile("/model.save");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                            .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                            .AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
            //.AddSingleton<IFileProvider, FileProvider>()
            //.AddTransient<IEmailSender, EmailSender>();
            services.AddRepositories()
                    .AddServices();
            ConfigureAdditionalServices(services);
            services.AddSignalR();
            services.AddAutoMapper(typeof(DTOsProfile));
            //services.AddAutoMapperProfiles();

        }
        protected virtual void ConfigureAdditionalServices(IServiceCollection services)
        {
        }
        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new SecurityHeadersAttribute());
                options.Filters.Add(new AutoValidateModelStateAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
                // enleve l'information que l'antiforgery est celui d'aspnetcore
                options.Cookie.Name = "AntiforgeryToken";
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });
            // Configuration de Identity (supprimée de cet exemple)
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                // enleve l'information que le token d'authentification est celui de Identity
                options.Cookie.Name = "AuthenticationToken";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(1, 0, 0);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper autoMapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Port ssl configurable via la variable d'environnement : ASPNETCORE_https_port
            app.UseHttpsRedirection();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            // Fichiers statiques non accessibles si l'url commence par /api
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    // Permettre la mise en cache des fichiers statiques par le navigateur
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(365)
                    };
                }
            });
            app.UseMiddleware<NoCacheMiddleware>();
            // Authentification et MVC
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/");
            });
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
