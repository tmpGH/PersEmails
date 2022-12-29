using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Emails.Commands
{
    public class AddEmailToPersonCommand : ICommand
    {
        public string EmailAddress { get; set; }
        public int PersonId { get; set; }

        public int Execute(IAppContext context)
        {
            // TODO: sprawdzic czy sie doda do osoby
            context.Emails.Add(new Email
            {
                EmailAddress = EmailAddress,
                PersonId = PersonId
            });

            return context.SaveChanges();
        }
    }
}
