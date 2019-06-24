using Foundation.Messaging;

namespace Foundation.CQRS
{
    public abstract class Command :  ICommand
    {
        protected Command() 
        {
        }
    }
}