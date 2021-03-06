using Foundation.Data;
using Microsoft.EntityFrameworkCore;

namespace Foundation.CustomForm
{
    public class CustomFormDbContext : DbContext
    {
        public DbSet<CustomForm> CustomForms { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public CustomFormDbContext(DbContextOptions<CustomFormDbContext> options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomForm>().Property(x => x.Fields).IsJson();
            modelBuilder.Entity<Setting>().Property(x => x.Items).IsJson();
            base.OnModelCreating(modelBuilder);
        }
    }
}