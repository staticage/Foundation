using Foundation.CustomForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    public class CustomFormDbContextFactory : IDesignTimeDbContextFactory<CustomFormDbContext>
    {
        public CustomFormDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder();
            
            var optionsBuilder = new DbContextOptionsBuilder<CustomFormDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=efcore_test;User Id=postgres;Password=postgres;",
                options => options.MigrationsAssembly(typeof(CustomFormDbContextFactory).Assembly.FullName));

            return new CustomFormDbContext(optionsBuilder.Options);
        }
    }
}