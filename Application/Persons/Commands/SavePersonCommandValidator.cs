using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Persons.Commands
{
    public class SavePersonCommandValidator : IValidatorAsync<SavePersonCommand>
    {
        private readonly IAppContext _context;
        private readonly ILogger<SavePersonCommandValidator> _logger;

        public SavePersonCommandValidator(IAppContext context, ILogger<SavePersonCommandValidator> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> IsValid(SavePersonCommand command)
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
            var entity = await _context.Persons.FindAsync(command.Id);
            if (entity == null)
            {
                _logger.Log(LogLevel.Error, $"Person with id {command.Id} not found");
                return false;
            }

            return true;
        }
    }
}
