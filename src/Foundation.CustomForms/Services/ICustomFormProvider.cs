using System.Collections.Generic;
using System.Reflection;

namespace Foundation.CustomForm.Services
{
    public interface ICustomFormProvider
    {
        IEnumerable<CustomFormMetadata> FindCustomFormMetadata(Assembly assembly);
        IEnumerable<CustomForm> FindCustomFormSettings();
        CustomForm GetCustomFormSetting(string name);
    }
}