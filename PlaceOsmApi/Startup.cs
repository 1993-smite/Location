using FluentValidation.AspNetCore;
using Itinero;
using LocationOsmApi.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PlaceOsmApi.Services;
using PlaceOsmApi.Services.RouteService;
using PlaceOsmApi.Services.RouteService.ItineroRouteService;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace LocationOsmApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IConfigurationSection ConfigurationDadata => Configuration.GetSection("Dadata");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
               .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PlaceValidator>());

            
            services.AddResponseCaching();
            services.AddMemoryCache();

            var xmlDoc = string.Format(@"{0}PlaceOsmApi.XML", AppDomain.CurrentDomain.BaseDirectory);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Place OSM Api",
                    Description = "Place osm service api"
                });
                c.IncludeXmlComments(xmlDoc);

            });

            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            });

            var token = ConfigurationDadata.GetValue<string>("Token");
            services.AddScoped<IGeoLocationService, DadataService>(
                (provider)=> new DadataService(token)
            );

            var osrmLink = Configuration.GetValue<string>("OsrmApiLink");
            var itineroFilePath = Configuration.GetValue<string>("ItineroOsmFilePath");
            services.AddScoped<IMapManager, MapManager>(
                (provider) => new MapManager(
                    new List<IRouteService>() { 
                        new OsrmService(osrmLink)
                        , new ItineroService(itineroFilePath, MemoryCache.Default) })
            );

            services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(
                (provider) => ConnectionMultiplexer.Connect("localhost")
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();
            //app.UseRouting();

            //app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Map API");
                c.RoutePrefix = string.Empty;
            });

            //app.UseResponseCaching();

            // подключаем CORS
            //app.UseCors(builder => builder.AllowAnyOrigin()
            //                              .AllowAnyHeader()
            //                              .AllowAnyMethod());

            app.UseMvcWithDefaultRoute();
        }
    }
}
