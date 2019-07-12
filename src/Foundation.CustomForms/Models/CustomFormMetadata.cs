using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.CustomForm.Attributes;

namespace Foundation.CustomForm
{
    public class CustomFormMetadata
    {
        public TypeMetadata TypeMetadata { get; set; }
        public List<PropertyMetadata> Fields { get; set; }
        public string Label { get; set; }
        
        public static CustomFormMetadata Create(Type type)
        {
            return new CustomFormMetadata
            {
                TypeMetadata = TypeMetadata.Create(type),
                Label  = type.GetCustomAttribute<CustomFormAttribute>().Label,
                Fields = type.GetProperties().Where(x=> x.GetCustomAttribute<FormFieldAttribute>()!=null).Select(PropertyMetadata.Create).ToList()
            };
        }
    }
}