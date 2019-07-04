using System;

namespace Foundation.CustomForm
{
    public class TypeMetadata
    {
        public string Assembly { get; set; }
        public string Type { get; set; }
        public string AssemblyQualifiedName { get; set; } 
        public string Name { get; set; }
        
        public static TypeMetadata Create(Type type)
        {
            return new TypeMetadata
            {
                Assembly = type.Assembly.GetName().Name,
                Type = type.FullName,
                AssemblyQualifiedName = type.AssemblyQualifiedName,
                Name = type.Name
            };
        }
    }
}