using System;
using System.Collections.Generic;

namespace Foundation.DDD
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        IEnumerable<IAggregateRootEvent> GetChanges();
        void LoadHistory(IEnumerable<IAggregateRootEvent> events);
        int Version { get;}
        void AcceptChanges();
    }
}