using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkflowCore;
using WorkflowCore.Interface;

namespace Foundation.Workflow
{
    
    
    internal class WorkflowPersistence : IPersistenceProvider
    {
        private readonly WorkflowEngineDbContext _dbContext;

        public WorkflowPersistence(WorkflowEngineDbContext dbContext)
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

        public Task<List<WorkflowInstance>> GetRunnableInstances(DateTime now)
        {
            throw new NotImplementedException();
        }

        public void EnsureStoreExists()
        {
            _dbContext.Database.EnsureCreated();
        }


        public Task<WorkflowInstance> GetWorkflowInstance(string id)
        {
            return GetWorkflowInstance(Guid.Parse(id));
        }
    }
}