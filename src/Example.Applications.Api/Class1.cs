using Example.Applications.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Example.Applications.Api
{
    public class ExampleDbContextFactory : IDesignTimeDbContextFactory<ExampleDbContext>
    {
        public ExampleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExampleDbContext>();
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=custom_form_tests;User Id=postgres;Password=postgres;",
                options => options.MigrationsAssembly(typeof(ExampleDbContext).Assembly.FullName));

            return new ExampleDbContext(optionsBuilder.Options);
        }
    }
}