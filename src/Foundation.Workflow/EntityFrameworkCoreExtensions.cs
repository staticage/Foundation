using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Foundation.Workflow
{
    public  static class EntityFrameworkCoreExtensions
    {
        public static PropertyBuilder<TProperty> IsJson<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, TypeNameHandling typeNameHandling = TypeNameHandling.None) =>
            propertyBuilder.HasConversion(
                x => JsonConvert.SerializeObject(x, new JsonSerializerSettings{ TypeNameHandling = typeNameHandling}), 
                x => JsonConvert.DeserializeObject<TProperty>(x, new JsonSerializerSettings { TypeNameHandling = typeNameHandling }));
        
         
    }
}