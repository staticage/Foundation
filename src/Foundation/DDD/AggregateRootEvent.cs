using System;

namespace Foundation.DDD
{
    public abstract class AggregateRootEvent : IAggregateRootEvent
    {
        public Guid AggregateRootId { get; set; }
        public int Version { get; set; }
        public DateTime EventTime { get; set; }
    }
} 