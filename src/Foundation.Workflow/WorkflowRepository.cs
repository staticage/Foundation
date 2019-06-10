using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Workflow
{
    
    
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly WorkflowDbContext _dbContext;

        public WorkflowRepository(WorkflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddWorkflow(Workflow workflow)
        {
            await _dbContext.Workflows.AddAsync(workflow);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Workflow> GetWorkflow(Guid workflowId)
        {
            return _dbContext.Workflows.Include(x => x.ExecutionPointers).SingleOrDefaultAsync(x => x.Id == workflowId);
        }
    }
}