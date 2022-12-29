using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using System.Net.Mail;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailToPersonCommand : ICommandAsync
    {
        private readonly ILogger<AddEmailToPersonCommand> logger;

        public EmailDto Email { get; set; }

        public AddEmailToPersonCommand(ILogger<AddEmailToPersonCommand> logger) => this.logger = logger;

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            if(!IsEmailAddressValid())
                return 0;

            var person = await context.Persons.FindAsync(Email.PersonId);
            if (person == null)
            {
                logger.Log(LogLevel.Error, $"Person with id {Email.PersonId} not found");
                return 0;
            }

            context.Emails.Add(MapToEntity(Email, person));

            return context.SaveChanges();
        }

        private Email MapToEntity(EmailDto email, Person person)
        {
            return new Email
            {
                EmailAddress = email.EmailAddress,
                Person = person
            };
        }

        private bool IsEmailAddressValid()
        {
            if( Email == null)
            {
                logger.Log(LogLevel.Error, "Empty object: Email");
                return false;
            }

            Email.EmailAddress = Email.EmailAddress.Trim();
            if (string.IsNullOrWhiteSpace(Email.EmailAddress))
            {
                logger.Log(LogLevel.Error, "Empty field: EmailAddress");
                return false;
            }

            try
            {
                MailAddress mail = new MailAddress(Email.EmailAddress);
                return true;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, "Wrong email address");
                return false;
            }
        }
    }
}
