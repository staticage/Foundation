using AutoMapper;
using Example.Applications.Api.Common;
using Example.Applications.Domain;
using Foundation.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "Security.Bearer",
            ValidAudience = "Security.Bearer",
            IssuerSigningKey = JwtSecurityKey.Get()
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            }
        };
    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim("UserId"));
            });

            services.AddServiceBus();
            services.AddDbContext<ExampleDbContext>(opt =>
            {
                opt.UseNpgsql("Server=127.0.0.1;Port=5432;Database=testdb;User Id=postgres;Password=sasa;",
                    options => options.MigrationsAssembly(typeof(Startup).Assembly.FullName));
            });
            var configuration = new MapperConfiguration(cfg =>
            {
                // cfg.CreateMap<Foo, FooDto>();
                // cfg.CreateMap<Bar, BarDto>();
            });
            services.AddSingleton<IMapper>(configuration.CreateMapper());
            services.AddHealthChecks();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health");
            });
        }
    }
}