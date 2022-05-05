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
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Netwealth.Data;
using NetWealth.Data;

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
            services.AddTransient<NetWealthDbSeeder>();

            //Configure Currency Api Settings
            services.Configure<CurrencyConverterSettings>(Configuration.GetSection("CurrencyConverterSettings"));

            // ALLOW CORS (ONLY FOR ANGULAR DEBUG)
            var azureCorsUrl = Configuration.GetSection("AzureWebApiUrl").Value;
            services.AddCors(o => o.AddPolicy("CurrencyPolicy", builder =>
            {
                
                builder.WithOrigins(azureCorsUrl).AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            //services.AddMediatR(typeof(CurrencyConverterCommand).GetTypeInfo().Assembly);


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.RegisterRequestHandlers();


            //services.AddScoped(typeof(ICurrencyConverterService), typeof(CurrencyConverterService));


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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
