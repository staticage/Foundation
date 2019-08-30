using Microsoft.EntityFrameworkCore;

namespace Foundation.Workflow
{
    public class WorkflowDbContext : DbContext
    {
        public DbSet<WorkflowInstance> Workflows { get; set; }
        public DbSet<ExecutionPointer> ExecutionPointers { get; set; }

        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExecutionPointer>().Property(x => x.PublishedEvents).IsJson();
            base.OnModelCreating(modelBuilder);
        }
    }
}