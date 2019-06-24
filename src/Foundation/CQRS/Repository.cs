using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation.CQRS.EventStores;
using Foundation.DDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Foundation.CQRS
{
    public class Repository<TDbContext> : IRepository where TDbContext : DbContext
    {
        private readonly IEventStorePublisher _publisher;
        private readonly IEventStore _eventStore;
        private readonly TDbContext _dbContext;
        private IDictionary<Guid, IAggregateRoot> _cache;

        public Repository(IEventStorePublisher publisher, IEventStore eventStore, TDbContext dbContext)
        {
            _publisher = publisher;
            _eventStore = eventStore;
            _dbContext = dbContext;
            _cache = new Dictionary<Guid, IAggregateRoot>();


        }

        public async Task<TAggregateRoot> GetAsync<TAggregateRoot>(Guid id) where TAggregateRoot : class, IAggregateRoot
        {
            if (_cache.TryGetValue(id, out var aggregateRoot))
            {
                return (TAggregateRoot)aggregateRoot;
            }
            aggregateRoot = await _dbContext.Set<TAggregateRoot>().SingleOrDefaultAsync(x => x.Id == id);
            _cache.Add(id, aggregateRoot);

            return (TAggregateRoot)aggregateRoot;
        }

        public async Task<TAggregateRoot> GetAsync<TAggregateRoot>(Guid id, int version) where TAggregateRoot : class, IAggregateRoot
        {
            var aggregateRoot = (TAggregateRoot)Activator.CreateInstance(typeof(TAggregateRoot), true);
            var history = await _eventStore.GetEventsAsync(id, 1, version);
            aggregateRoot.LoadHistory(history);
            return aggregateRoot;
        }

        public async Task AddAsync(IAggregateRoot aggregateRoot)
        {
            await SaveAggregate(aggregateRoot);

            await _dbContext.AddAsync(aggregateRoot);
            _cache.Add(aggregateRoot.Id, aggregateRoot);
        }

        public async Task SaveChangesAsync()
        {
            foreach (var aggregateRoot in _cache.Values)
            {
                await SaveAggregate(aggregateRoot);
            }
            await _dbContext.SaveChangesAsync();
            await _publisher.AcceptAsync();
        }

        public Task<IDbContextTransaction> GetTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }

        private async Task SaveAggregate(IAggregateRoot aggregateRoot)
        {
            var newEvents = aggregateRoot.GetChanges().ToList();
            await _eventStore.SaveAsync(newEvents);
            newEvents.ForEach(e => _publisher.Publish(e));
            aggregateRoot.AcceptChanges();
        }
    }
}