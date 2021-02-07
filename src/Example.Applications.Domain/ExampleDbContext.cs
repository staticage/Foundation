using Foundation.Core;
using Microsoft.EntityFrameworkCore;

namespace Example.Applications.Domain
{
    public class ExampleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        // public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ArticleAccessRecord> ArticleAccessRecords { get; set; }

        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {
        }
    }
}