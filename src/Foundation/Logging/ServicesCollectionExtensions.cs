using System;
using Foundation.Logging.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Logging
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddLoggingFetcher(this IServiceCollection @this, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            @this.AddDbContext<LogsDbContext>(optionsBuilder);
            @this.AddScoped<ILogFetcher, LogFetcher>();
            return @this;
        }
    }
}