using System;
using System.Collections.Generic;
using System.Reflection;
using Foundation.CustomForm.Attributes;

namespace Foundation.CustomForm
{
    public class CustomFormMetadata
    {
        public TypeMetadata TypeMetadata { get; set; }
        public List<PropertyMetadata> Properties { get; set; }
        public string Name { get; set; }
        public static CustomFormMetadata Create(Type type)
        {
            return new CustomFormMetadata
            {
                TypeMetadata = TypeMetadata.Create(type),
                Name  = type.GetCustomAttribute<CustomFormAttribute>().Name
            };
        }
    }
}