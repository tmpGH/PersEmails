using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using System.Net.Mail;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailToPersonCommand : ICommandAsync
    {
        private EmailDto email;

        public AddEmailToPersonCommand(EmailDto email)
        {
            this.email = email;
        }

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            if(!IsEmailAddressValid())
                return 0;

            var person = await context.Persons.FindAsync(email.PersonId);
            if (person == null)
                return 0;

            context.Emails.Add(MapToEntity(email, person));

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
            if (email == null)
                return false;
            
            email.EmailAddress = email.EmailAddress.Trim();
            if (string.IsNullOrWhiteSpace(email.EmailAddress) == null)
                return false;

            try
            {
                MailAddress mail = new MailAddress(email.EmailAddress);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
