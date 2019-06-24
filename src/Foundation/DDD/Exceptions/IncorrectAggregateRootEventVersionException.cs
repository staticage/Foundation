using Foundation.Exceptions;

namespace Foundation.DDD.Exceptions
{
    public class IncorrectAggregateRootEventVersionException : DomainException
    {
        public IncorrectAggregateRootEventVersionException() : base("Invalid AggregateRootEvent Version")
        {
        }
    }
}