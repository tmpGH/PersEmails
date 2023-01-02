using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;
using System.Net.Mail;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailToPersonCommandValidator : IValidator<AddEmailToPersonCommand>
    {
        private readonly IAppContext _context;
        private readonly ILogger<AddEmailToPersonCommandValidator> _logger;

        public AddEmailToPersonCommandValidator(IAppContext context, ILogger<AddEmailToPersonCommandValidator> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool IsValid(AddEmailToPersonCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.EmailAddress))
            {
                _logger.Log(LogLevel.Error, "Empty field: EmailAddress");
                return false;
            }

            command.EmailAddress = command.EmailAddress.Trim();
            try
            {
                MailAddress mail = new MailAddress(command.EmailAddress);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, "Wrong email address");
                return false;
            }

            var person = _context.Persons.Find(command.PersonId);
            if (person == null)
            {
                _logger.Log(LogLevel.Error, $"Person with id {command.PersonId} not found");
                return false;
            }

            return true;
        }
    }
}
