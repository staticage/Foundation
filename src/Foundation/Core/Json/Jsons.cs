using Newtonsoft.Json.Linq;

namespace Foundation.Core.Json
{
    public static class Jsons
    {
        public static T ToObject<T>(object jsonObject)
        {
            switch (jsonObject)
            {
                case null:
                    return default(T);
                case JObject jObject:
                    return jObject.ToObject<T>();
                case T t:
                    return t;
                default:
                    return default(T);
            }
        }
    }
}