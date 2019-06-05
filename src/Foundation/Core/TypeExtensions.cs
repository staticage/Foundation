using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Foundation.Core
{
    public static class TypeExtensions
    {
        static readonly Type[] SystemTypes = {
                                                 typeof(Guid),
                                                 typeof(string),
                                                 typeof(IEnumerable),
                                             };

        public static bool IsImplementsInterface(this Type type, Type interfaceType)
        {
            var types = new List<Type> {type}.Concat(type.GetInterfaces());
            return types.Any(x => x == interfaceType || (x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType));
        }

        public static bool IsCustomType(this Type type)
        {
            var isSystemType = type.IsPrimitive || SystemTypes.Contains(type) ;
            return !isSystemType;
        }
    }
}
