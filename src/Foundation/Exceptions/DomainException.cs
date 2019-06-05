using System;

namespace Foundation.Exceptions
{
    public interface IError
    {
        string Message { get; }
        string Code { get; }
    }

    public class AggregateRootException : DomainException
    {
        public Guid AggregateRootId { get; }

        public AggregateRootException(Guid aggregateRootId, string message) : base(
            $"AggregateId: {aggregateRootId} {message}")
        {
            AggregateRootId = aggregateRootId;
        }
    }

    public class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
        }
    }

    public class InvalidActionException : DomainException
    {
        public InvalidActionException(string message) : base(message)
        {
        }
    }

    public class DomainValidationException : DomainException
    {
        public DomainValidationException(string message) : base(message)
        {
        }
    }

    public class LoginDeviceNotMatchValidationException : DomainValidationException
    {
        public Guid Id { get; set; }

        public LoginDeviceNotMatchValidationException(string message) : base(message)
        {
        }
    }

    public class NoPermissionsException : DomainException,IError
    {
        public NoPermissionsException(string message) : base(message)
        {
        }

        public string Code => "4001";
    }

    public class ValidationException : ApplicationException
    {
    }

    public class UnauthorizedException : DomainException
    {
        public UnauthorizedException(string message) : base(message)
        {
        }
    }
}