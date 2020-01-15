using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;

namespace Foundation.Workflow
{
    public static class ServicesExtensions
    {
        public static void AddWorkflow(this IServiceCollection services)
        {
            services.AddScoped<IPersistenceProvider, WorkflowPersistence>();
            services.AddScoped<UserTaskStepBody, UserTaskStepBody>();
            services.AddSingleton<IWorkflowDefinitionRegistrar, WorkflowDefinitionRegistrar>();
            services.AddSingleton<IWorkflowHost, WorkflowEngine>();
            services.AddSingleton<IWorkflowExecutor, WorkflowExecutor>();
        } 
    }
}