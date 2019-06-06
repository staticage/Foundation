using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Foundation.Workflow.Tests
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<WorkflowDbContext>
    {
        public WorkflowDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WorkflowDbContext>();
            optionsBuilder.UseSqlServer("Server=62.234.214.209,1445;Database=rebus_lyh;User Id=sa;Password=sasa@123;",
                options => options.MigrationsAssembly(typeof(WorkflowDbContext).Assembly.FullName));

            return new WorkflowDbContext(optionsBuilder.Options);
        }
    }
}