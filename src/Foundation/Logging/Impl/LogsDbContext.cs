using System;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Logging.Impl
{
    internal class LogsDbContext : DbContext
    {
        public DbQuery<LogEntry> Logs { get; set; }
        public LogsDbContext(DbContextOptions<LogsDbContext> options) : base(options)
        {
        }
    }

    public class LogEntry
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
        public string Application { get; set; }
        
    }
}