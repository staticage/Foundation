using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Applications.Domain;
using Foundation.CustomForm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Example.Applications.Api
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
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:8080")
                    .AllowCredentials();
            }));
            // services.AddCustomForm(x=> x.UseNpgsql("Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=postgres;"));
            // services.AddDbContext<ExampleDbContext>(options =>
            //     {
            //         options.UseNpgsql(
            //             "Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=postgres;",
            //             opt => opt.MigrationsAssembly(typeof(Startup).Assembly.FullName));
            //     }
            // );
            services.AddHealthChecks();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}