using System.Linq;
using Foundation.Logging.Impl;

namespace Foundation.Logging
{
    public interface ILogFetcher
    {
        IQueryable<LogEntry> Query();
    }
}