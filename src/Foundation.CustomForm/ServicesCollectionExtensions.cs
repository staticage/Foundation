using System;
using System.Linq;
using Foundation.CustomForm.Services;
using Foundation.CustomForm.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.CustomForm
{
    
    
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddCustomForm(this IServiceCollection services, Action<CustomFormOptions> setupAction = null)
        {
            if (services.Any(x => x.ServiceType == typeof(CustomFormOptions)))
                throw new InvalidOperationException("Custom form services already registered");
            
            var options = new CustomFormOptions();
            setupAction?.Invoke(options);
            services.AddSingleton(options);
            services.AddSingleton<ICustomFormProvider, CustomFormProvider>();
            return services;
        }
    }

    public class CustomFormOptions
    {
    }
}