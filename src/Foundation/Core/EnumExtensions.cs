using System;
using System.ComponentModel;
using System.Reflection;

namespace Foundation.Core
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T @this) where T : IConvertible
        {
            Type temType = @this.GetType();
            MemberInfo[] memberInfos = temType.GetMember(@this.ToString());
            if (memberInfos.Length > 0)
            {
                object[] objs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs.Length > 0)
                {
                    return ((DescriptionAttribute)objs[0]).Description;
                }
            }
            return @this.ToString();
        }

        public static T GetEunmByDescription<T>(this string @this)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == @this)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == @this)
                        return (T)field.GetValue(null);
                }
            }
            return default(T);
        }

        public static T GetEunmByName<T>(this string @this)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (field.Name == @this)
                    return (T)field.GetValue(null);
            }
            return default(T);
        }
    }
}
