using System;

namespace Foundation.DDD
{
    public interface IAggregateRootEvent : IDomainEvent
    {
        Guid AggregateRootId { get; set; }
        int Version { get; set; }
        DateTime EventTime { get; set; }
    }
}