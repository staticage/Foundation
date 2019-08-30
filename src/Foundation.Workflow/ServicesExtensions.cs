using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Workflow
{
    public static class ServicesExtensions
    {
        public static void AddWorkflow(this IServiceCollection services)
        {
            
            services.AddScoped<IWorkflowPersistence, WorkflowPersistence>();
        } 
    }
}