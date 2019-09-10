using Foundation.Core;
using Microsoft.EntityFrameworkCore;

namespace Example.Applications.Domain
{
    public class ExampleDbContext : DbContext
    {
         public DbSet<Customer> Customers { get; set; }
         public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
         {
         }
    }
}