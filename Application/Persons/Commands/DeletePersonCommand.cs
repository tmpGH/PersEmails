using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Persons.Commands
{
    public class DeletePersonCommand : ICommandAsync
    {
        private int personId;

        public DeletePersonCommand(int personId)
        {
            this.personId = personId;
        }

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            var entity = await context.Persons.FindAsync(personId);
            if (entity == null)
                return 0;

            foreach (var email in entity.Emails)
            {
                context.Emails.Remove(email);
            }
            context.Persons.Remove(entity);

            return context.SaveChanges();
        }
    }
}
