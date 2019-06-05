using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Logging.Impl
{
    internal class LogFetcher : ILogFetcher
    {
        private readonly LogsDbContext _logsDbContext;

        public LogFetcher(LogsDbContext logsDbContext)
        {
            _logsDbContext = logsDbContext;
        }
        
        public IQueryable<LogEntry> Query()
        {
            return _logsDbContext.Logs.FromSql("SELECT * FROM logs");
        }
    }
}