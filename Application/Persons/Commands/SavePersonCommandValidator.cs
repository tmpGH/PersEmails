using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Persons.Commands
{
    public class SavePersonCommandValidator
    {
        private readonly IAppContext context;
        private readonly ILogger<SavePersonCommandValidator> logger;

        public SavePersonCommandValidator(IAppContext context, ILogger<SavePersonCommandValidator> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        private async Task<bool> IsValid(SavePersonCommand command)
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
            var entity = await context.Persons.FindAsync(command.Id);
            if (entity == null)
            {
                logger.Log(LogLevel.Error, $"Person with id {command.Id} not found");
                return false;
            }

            return true;
        }
    }
}
