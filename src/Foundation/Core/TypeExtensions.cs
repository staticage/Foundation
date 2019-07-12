using System;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Core
{
    public static class TypeExtensions
    {
        public static bool IsImplementsInterface(this Type type, Type interfaceType)
        {
            var types = new List<Type> {type}.Concat(type.GetInterfaces());
            return types.Any(x => x == interfaceType || (x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType));
        }

        public static string GetDisplayName(this Type type)
        {
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return $"{GetDisplayName(type.GetGenericArguments()[0])}?";
                }
                return $"{type.Name.Remove(type.Name.IndexOf('`'))}<{string.Join(", ", type.GetGenericArguments().Select(at => at.GetDisplayName()))}>";
            }
            return type.Name;
        }
        
        public static object GetDefaultValue(this Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}
