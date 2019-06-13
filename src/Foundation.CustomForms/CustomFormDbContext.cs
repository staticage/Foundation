using Foundation.Data;
using Microsoft.EntityFrameworkCore;

namespace Foundation.CustomForm
{
    public class CustomFormDbContext : DbContext
    {
        public DbSet<CustomForm> CustomForms { get; set; }
        public CustomFormDbContext(DbContextOptions<CustomFormDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomForm>().Property(x => x.FieldGroups).IsJson();
            base.OnModelCreating(modelBuilder);
        }
    }
}