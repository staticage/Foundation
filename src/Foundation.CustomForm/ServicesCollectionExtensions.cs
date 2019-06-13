using System;
using System.Linq;
using Foundation.CustomForm.Services;
using Foundation.CustomForm.Services.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Foundation.CustomForm
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddCustomForm(this IServiceCollection services, Action<CustomFormOptions> setupAction = null)
        {
            if (services.Any(x => x.ServiceType == typeof(CustomFormOptions)))
                throw new InvalidOperationException("Custom form services already registered");

            var options = new CustomFormOptions(services);
            setupAction?.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<ICustomFormProvider, CustomFormProvider>();
            return services;
        }
    }

    public class CustomFormOptions
    {
        private readonly IServiceCollection _services;

        public CustomFormOptions(IServiceCollection services)
        {
            _services = services;
        }

        public CustomFormOptions UseNpgsql(string connectionString, Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction = null)
        {
            _services.AddDbContext<CustomFormDbContext>(cfg => { cfg.UseNpgsql(connectionString, npgsqlOptionsAction); });
            return this;
        }
    }
}