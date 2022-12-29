using Microsoft.Extensions.Logging;

namespace PersEmails.Application.Persons.Commands
{
    public class AddPersonCommandValidator
    {
        private readonly ILogger<AddPersonCommandValidator> logger;

        public AddPersonCommandValidator(ILogger<AddPersonCommandValidator> logger)
            => this.logger = logger;

        private bool IsValid(AddPersonCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                logger.Log(LogLevel.Error, "Empty field: Name");
                return false;
            }
            if (string.IsNullOrWhiteSpace(command.Surname))
            {
                logger.Log(LogLevel.Error, "Empty field: Surname");
                return false;
            }
            if (command.Name.Length > 50)
            {
                logger.Log(LogLevel.Error, "Too long field: Name");
                return false;
            }
            if (command.Surname.Length > 50)
            {
                logger.Log(LogLevel.Error, "Too long field: Surname");
                return false;
            }

            return true;
        }
    }
}
