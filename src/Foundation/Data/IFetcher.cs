using System.Linq;
using System.Threading.Tasks;

namespace Foundation.Data
{
    public interface IFetcher
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
    }

    public static class FetcherExtensions
    {
        
    } 
}
