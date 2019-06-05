using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Foundation.Core.Json.Convertors
{
    public class LocalDateTimeConverter : IsoDateTimeConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // To Local Time
            if (value is DateTime time)
            {
                var newValue = time;
                base.WriteJson(writer, newValue, serializer);
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    }
}