using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Workflow
{
    
    
    internal class WorkflowPersistence : IWorkflowPersistence
    {
        private readonly WorkflowDbContext _dbContext;

        public WorkflowPersistence(WorkflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddWorkflow(WorkflowInstance workflow)
        {
            await _dbContext.Workflows.AddAsync(workflow);
            await _dbContext.SaveChangesAsync();
        }

        public Task<WorkflowInstance> GetWorkflowInstance(Guid workflowId)
        {
            return _dbContext.Workflows.Include(x => x.ExecutionPointers).SingleOrDefaultAsync(x => x.Id == workflowId);
        }

        public Task<WorkflowInstance> GetWorkflowInstance(string id)
        {
            return GetWorkflowInstance(Guid.Parse(id));
        }
    }
}