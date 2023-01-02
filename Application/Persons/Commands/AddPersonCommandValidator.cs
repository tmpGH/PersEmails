using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Persons.Commands
{
    public class AddPersonCommandValidator : IValidator<AddPersonCommand>
    {
        private readonly ILogger<AddPersonCommandValidator> _logger;

        public AddPersonCommandValidator(ILogger<AddPersonCommandValidator> logger) => _logger = logger;

        public bool IsValid(AddPersonCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                _logger.Log(LogLevel.Error, "Empty field: Name");
                return false;
            }
            if (string.IsNullOrWhiteSpace(command.Surname))
            {
                _logger.Log(LogLevel.Error, "Empty field: Surname");
                return false;
            }
            if (command.Name.Length > 50)
            {
                _logger.Log(LogLevel.Error, "Too long field: Name");
                return false;
            }
            if (command.Surname.Length > 50)
            {
                _logger.Log(LogLevel.Error, "Too long field: Surname");
                return false;
            }

            return true;
        }
    }
}
