using Foundation.CustomForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tests
{
    public class CustomFormDbContextFactory : IDesignTimeDbContextFactory<CustomFormDbContext>
    {
        public CustomFormDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomFormDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=postgres;",
                options => options.MigrationsAssembly(typeof(CustomFormDbContext).Assembly.FullName));

            return new CustomFormDbContext(optionsBuilder.Options);
        }
    }
}