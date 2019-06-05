using System.Linq;
using FluentValidation;
using Foundation.Exceptions;
using Foundation.Messaging;

namespace Foundation.CQRS
{

    public static class ValidatorExtensions
    {
        public static void ValidateCommand(this IValidator validator, ICommand command)
        {
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new CommandValidationException(string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage)));
            }
        }
    }
}
