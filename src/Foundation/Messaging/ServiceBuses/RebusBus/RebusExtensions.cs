using Foundation.Messaging.ServiceBuses.RebusBus.Steps;
using Rebus.Config;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;
using Rebus.Pipeline.Send;

namespace Foundation.Messaging.ServiceBuses.RebusBus
{
    public static class RebusExtensions
    {
        public static void PrepareOutgoingMessageHeader(this OptionsConfigurer configurer)
        {
            configurer.Decorate<IPipeline>(c =>
            {
                var pipeline = c.Get<IPipeline>();
                var step = new WriteEnvironmentNameOutgoingStep();

                return new PipelineStepInjector(pipeline).OnSend(step, PipelineRelativePosition.Before, typeof(SerializeOutgoingMessageStep));
            });
        }
        
        public static void VerifyIncomingMessageHeader(this OptionsConfigurer configurer)
        {
            configurer.Decorate<IPipeline>(c =>
            {
                var pipeline = c.Get<IPipeline>();
                var step = new VerifyEnvironmentNameIncomingStep();
                
                return new PipelineStepInjector(pipeline).OnReceive(step, PipelineRelativePosition.After, typeof(DeserializeIncomingMessageStep));
            });
        }
        
        public static void EnableEntityFramework(this OptionsConfigurer configurer)
        {
            configurer.Decorate<IPipeline>(c =>
            {
                var pipeline = c.Get<IPipeline>();
                var step = new EntityFrameworkSaveChangesStep();
                
                return new PipelineStepInjector(pipeline).OnReceive(step, PipelineRelativePosition.After, typeof(DispatchIncomingMessageStep));
            });
        }
    }
}