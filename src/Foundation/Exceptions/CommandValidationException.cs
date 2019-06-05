using System;
using System.Collections.Generic;
using Foundation.Core;

namespace Foundation.Exceptions
{
    public class CommandValidationException : ApplicationException
    {
        public IEnumerable<string> ErrorMessages { get; set; }

        public CommandValidationException(IEnumerable<string> errorMessages)
            : base(errorMessages.JoinString(Environment.NewLine)) => ErrorMessages = errorMessages;

        public CommandValidationException(params string[] errorMessages)
            : this((IEnumerable<string>)errorMessages)
        {
        }
    }
}
