using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailCommand : ICommandAsync
    {
        private EmailDto email;

        public AddEmailCommand(EmailDto email)
        {
            this.email = email;
        }

        public async Task<int> ExecuteAsync(IAppContext context)
        {
            // TODO: walidacja emaila

            var person = await context.Persons.FindAsync(email.PersonId);
            // TODO: nieznaleziona persona
            var newEmail = new Email
            {
                EmailAddress = email.EmailAddress,
                Person = person
            };

            context.Emails.Add(newEmail);
            return context.SaveChanges();
        }
    }
}
