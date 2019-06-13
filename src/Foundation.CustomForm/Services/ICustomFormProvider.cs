using System.Collections.Generic;
using System.Reflection;

namespace Foundation.CustomForm.Services
{
    public interface ICustomFormProvider
    {
        IEnumerable<CustomFormMetadata> FindCustomFormMetadata(Assembly assembly);
        IEnumerable<CustomFormSetting> FindCustomFormSettings();
    }
}