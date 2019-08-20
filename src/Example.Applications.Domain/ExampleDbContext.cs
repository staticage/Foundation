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

         private void ConvertToSnakeCase(ModelBuilder modelBuilder)
         {
             foreach (var entity in modelBuilder.Model.GetEntityTypes())
             {
                 // Replace table names
                 entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                 // Replace column names            
                 foreach (var property in entity.GetProperties())
                 {
                     property.Relational().ColumnName = property.Name.ToSnakeCase();
                 }

                 foreach (var key in entity.GetKeys())
                 {
                     key.Relational().Name = key.Relational().Name.ToSnakeCase();
                 }

                 foreach (var key in entity.GetForeignKeys())
                 {
                     key.Relational().Name = key.Relational().Name.ToSnakeCase();
                 }

                 foreach (var index in entity.GetIndexes())
                 {
                     index.Relational().Name = index.Relational().Name.ToSnakeCase();
                 }
             }
         }
    }
}