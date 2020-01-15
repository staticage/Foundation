using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WorkflowCore;

namespace CareGarden.Applications.AccountCenter.Api
{
    public class WorkflowEngineDbContextFactory : IDesignTimeDbContextFactory<WorkflowEngineDbContext>
    {
        public WorkflowEngineDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder();
      

            var optionsBuilder = new DbContextOptionsBuilder<WorkflowEngineDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=62.234.214.209,1445;Database=new_workflow_db;User Id=sa;Password=sasa@123;",
                options => options.MigrationsAssembly(typeof(WorkflowEngineDbContext).Assembly.FullName));

            return new WorkflowEngineDbContext(optionsBuilder.Options);
        }
    }
}