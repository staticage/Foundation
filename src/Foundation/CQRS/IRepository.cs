using System;
using System.Threading.Tasks;
using Foundation.DDD;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foundation.CQRS
{
    public interface IRepository
    {
        Task<TAggregateRoot> GetAsync<TAggregateRoot>(Guid id) where TAggregateRoot : class, IAggregateRoot;
        Task<TAggregateRoot> GetAsync<TAggregateRoot>(Guid id, int version) where TAggregateRoot : class, IAggregateRoot;
        Task AddAsync(IAggregateRoot aggregateRoot);
        Task SaveChangesAsync();
    }
}