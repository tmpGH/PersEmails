using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;
using System.Net.Mail;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailToPersonCommandValidator : IValidator<AddEmailToPersonCommand>
    {
        private readonly IAppContext context;
        private readonly ILogger<AddEmailToPersonCommandValidator> logger;

        public AddEmailToPersonCommandValidator(IAppContext context, ILogger<AddEmailToPersonCommandValidator> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public bool IsValid(AddEmailToPersonCommand command)
        {
            command.EmailAddress = command.EmailAddress.Trim();
            if (string.IsNullOrWhiteSpace(command.EmailAddress))
            {
                logger.Log(LogLevel.Error, "Empty field: EmailAddress");
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(command.EmailAddress);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "Wrong email address");
                return false;
            }

            var person = context.Persons.Find(command.PersonId);
            if (person == null)
            {
                logger.Log(LogLevel.Error, $"Person with id {command.PersonId} not found");
                return false;
            }

            return true;
        }
    }
}
