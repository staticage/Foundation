using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.CustomForm.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.CustomForm.Services.Impl
{
    internal class CustomFormProvider : ICustomFormProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public IEnumerable<CustomFormMetadata> FindCustomFormMetadata(Assembly assembly) =>
            assembly.GetTypes()
                .Where(x => x.IsPublic && x.IsClass && !x.IsAbstract && x.GetCustomAttributes<CustomFormAttribute>().Any())
                .Select(CustomFormMetadata.Create).ToList();

        public CustomFormProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public IEnumerable<CustomForm> FindCustomFormSettings()
        {
            throw new NotImplementedException();
        }

        public CustomForm GetCustomFormSetting(string name)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                return scope.ServiceProvider.GetService<CustomFormDbContext>().CustomForms.SingleOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));    
            }
        }
    }
}