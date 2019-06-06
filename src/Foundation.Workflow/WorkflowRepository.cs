using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Foundation.Workflow
{
    
    
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly WorkflowDbContext _dbContext;
        IDictionary<string, List<WorkflowDefinition>> _definitions = new Dictionary<string, List<WorkflowDefinition>>();

        public WorkflowRepository(WorkflowDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task AddWorkflowDefinition(string workflowDefinitionId, WorkflowDefinition definition)
        {
            if (!_definitions.ContainsKey(workflowDefinitionId))
            {
                _definitions.Add(workflowDefinitionId, new List<WorkflowDefinition>());
            }

            _definitions[workflowDefinitionId].Add(definition);
            return Task.CompletedTask;
        }

        public Task<WorkflowDefinition> GetWorkflowDefinition(string workflowDefinitionId, int version)
        {
            var definitions = _definitions[workflowDefinitionId];
            var definition = version > 0 ? definitions[version - 1] : definitions.Last();
            return Task.FromResult(definition);
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