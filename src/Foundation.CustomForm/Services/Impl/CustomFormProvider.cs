using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.CustomForm.Attributes;

namespace Foundation.CustomForm.Services.Impl
{
    internal class CustomFormProvider : ICustomFormProvider
    {
        public IEnumerable<CustomFormMetadata> FindCustomForms(Assembly assembly) =>
            assembly.GetTypes()
                .Where(x => x.IsPublic && x.IsClass && !x.IsAbstract && x.GetCustomAttributes<CustomFormAttribute>().Any())
                .Select(CustomFormMetadata.Create).ToList();
    }
}