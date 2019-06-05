using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Foundation.Core;
using Foundation.DDD;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Foundation.CQRS.EventStores
{
    public abstract class EventsDbContext : DbContext
    {
        protected EventsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<EventRecord> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConvertToSnakeCase(modelBuilder);
        }

        private void ConvertToSnakeCase(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (entity.IsQueryType)
                {
                    continue;
                }
                   
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName;

                // Replace column names            
                foreach (var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Name;
                }

                foreach (var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name;
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name;
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is IEntity entity)
                    {
                        entity.LastModifiedOn = entity.CreatedOn = DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IEntity entity)
                    {
                        entity.LastModifiedOn = DateTime.Now;
                    }
                }
            }

            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is IEntity entity)
                    {
                        entity.LastModifiedOn = entity.CreatedOn = DateTime.Now;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is IEntity entity)
                    {
                        entity.LastModifiedOn = DateTime.Now;
                    }
                }
            }

            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }
    }

    public class EfCoreEventStore<TEventStoreDbContext> : IEventStore where TEventStoreDbContext : EventsDbContext
    {
        private readonly TEventStoreDbContext _dbContext;

        public EfCoreEventStore(TEventStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<IAggregateRootEvent>> GetEventsAsync(Guid aggregateRootId, int startVersion = 1, int endVersion = int.MaxValue)
        {
            var eventRecords = await _dbContext.Events
                .AsNoTracking()
                .Where(x => x.AggregateRootId == aggregateRootId && x.Version >= startVersion && x.Version <= endVersion)
                .OrderBy(x => x.Version)
                .ToListAsync();
            var events = new List<IAggregateRootEvent>();
            foreach (var eventRecord in eventRecords)
            {
                events.Add((IAggregateRootEvent) JsonConvert.DeserializeObject(eventRecord.Event, Type.GetType(eventRecord.EventType)));
            }

            return events;
        }

        public Task SaveAsync(IEnumerable<IAggregateRootEvent> events)
        {
            var records = events.Select(e => new EventRecord
            {
                EventTime = e.EventTime,
                AggregateRootId = e.AggregateRootId,
                Version = e.Version,
                Event = JsonConvert.SerializeObject(e),
                EventType = e.GetType().FullName + ", " + e.GetType().Assembly.GetName().Name
            }).ToList();

            return _dbContext.Events.AddRangeAsync(records);
        }
    }

    public class EventRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid AggregateRootId { get; set; }
        public string EventType { get; set; }
        public string Event { get; set; }
        public DateTime EventTime { get; set; }
        public int Version { get; set; }
    }

    public interface IEventStore
    {
        Task<IEnumerable<IAggregateRootEvent>> GetEventsAsync(Guid aggregateRootId, int startVersion = 1, int endVersion = int.MaxValue);
        Task SaveAsync(IEnumerable<IAggregateRootEvent> events);
    }
}