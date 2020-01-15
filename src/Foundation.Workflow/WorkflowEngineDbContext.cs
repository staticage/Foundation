using Foundation.Workflow;
using Microsoft.EntityFrameworkCore;

namespace WorkflowCore
{
    public class WorkflowEngineDbContext : DbContext
    {
        public DbSet<WorkflowInstance> Workflows { get; set; }
        public DbSet<ExecutionPointer> ExecutionPointers { get; set; }

        public WorkflowEngineDbContext(DbContextOptions<WorkflowEngineDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("wfc");
            modelBuilder.Entity<ExecutionPointer>()
                .ToTable("Workflow")
                .Property(x => x.PublishedEvents).IsJson();
            modelBuilder.Entity<ExecutionPointer>()
                .ToTable("ExecutionPointer")
                .Property(x => x.ExtensionAttributes).IsJson();
            base.OnModelCreating(modelBuilder);
        }
    }
}