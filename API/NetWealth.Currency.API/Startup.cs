using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NetWealth.Currency.API.Mapping;
using NetWealth.Currency.API.Middleware;
using Netwealth.Data;
using NetWealth.Data;
using NetWealth.Data.Models.Command;
using NetWealth.Data.Models.Utility;
using NetWealth.Domain;
using NetWealth.Repositories;
using NetWealth.Repositories.Implementation;
using Netwealth.Services;
using NetWealth.Services;
using NetWealth.Services.Implementation;
using QUBE.Web.API.Services;

namespace NetWealth.Currency.API
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
            //Get Settings from appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            //Config DB
            services.AddDbContext<NetwealthDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AzureConnectionString")));
            
            //Configure Currency Api Settings
            services.Configure<CurrencyConverterSettings>(Configuration.GetSection("CurrencyConverterSettings"));

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            //AutoMapper

            services.AddSingleton<Profile, DtoMappingProfile>();
            services.AddSingleton(provider =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    var profiles = provider.GetServices<Profile>();
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                });
                return config.CreateMapper();
            });

            services.AddTransient<NetWealthDbSeeder>();

            // ALLOW CORS (ONLY FOR ANGULAR DEBUG)
            var azureCorsUrl = Configuration.GetSection("AzureWebApiUrl").Value;
            services.AddCors(o => o.AddPolicy("CurrencyPolicy", builder =>
            {
                builder.WithOrigins(azureCorsUrl).AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegisterRequestHandlers();

            services.AddMediatR(typeof(CurrencyConverterCommand).GetTypeInfo().Assembly);

            services.AddScoped(typeof(ICurrencyConverterService), typeof(CurrencyConverterService));
            services.AddScoped(typeof(ICountryCurrencyRepository), typeof(CountryCurrencyRepository));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            // configure ip rate limiting middleware
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddInMemoryRateLimiting();
            services.AddMvc((options) =>
            {
                options.EnableEndpointRouting = false;

            }).AddNewtonsoftJson();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<NetWealthDbSeeder>();
                    seeder?.Seed().Wait();
                }
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CurrencyPolicy");

            app.UseAuthorization();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
