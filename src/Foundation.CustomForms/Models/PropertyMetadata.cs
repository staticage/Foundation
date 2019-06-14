using System.Reflection;
using Foundation.CustomForm.Attributes;

namespace Foundation.CustomForm
{
    public class PropertyMetadata
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string DefaultValue { get; set; }
        public string Type { get; set; }

        public static PropertyMetadata Create(PropertyInfo propertyInfo)
        {
            return new PropertyMetadata
            {
                Name = propertyInfo.Name,
                Label = propertyInfo.GetCustomAttribute<FormFieldAttribute>()?.Label ?? propertyInfo.Name
            };
        }
    }
}