using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

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
            if (email == null)
                return 0;
            // TODO: walidacja pola EmailAddress

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
    }
}
