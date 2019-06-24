using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Foundation.Data
{
    public  static class EntityFrameworkCoreExtensions
    {
        public static PropertyBuilder<TProperty> IsJson<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, TypeNameHandling typeNameHandling = TypeNameHandling.None) =>
            propertyBuilder.HasConversion(
                x => JsonConvert.SerializeObject(x, new JsonSerializerSettings{ TypeNameHandling = typeNameHandling}), 
                x => JsonConvert.DeserializeObject<TProperty>(x, new JsonSerializerSettings { TypeNameHandling = typeNameHandling }));
        
        public static PagedList<TSource> ToPagedList<TSource>(this IQueryable<TSource> query, int page, int pageSize)
        {
            return new PagedList<TSource>(page, pageSize, query.Count(), query.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public static async Task<PagedList<TSource>> ToPagedListAsync<TSource>(this IQueryable<TSource> @this, int page, int pageSize)
        {
            return new PagedList<TSource>(page, pageSize,
                await @this.CountAsync(),
                await @this.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        public static Task<PagedList<TSource>> ToPagedListAsync<TSource>(this IQueryable<TSource> @this, IPagedQuery query) => @this.ToPagedListAsync(query.Page, query.PageSize); 
    }
}