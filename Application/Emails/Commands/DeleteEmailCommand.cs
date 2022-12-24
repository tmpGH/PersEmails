using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Emails.Commands
{
    public class DeleteEmailCommand : ICommandAsync
    {
        private int emailId;

        public DeleteEmailCommand(int emailId)
        {
            this.emailId = emailId;
        }

        public async Task<int> ExecuteAsync(IAppContext context)
        {
            var entity = await context.Emails.FindAsync(emailId);

            context.Emails.Remove(entity);
            
            return context.SaveChanges();
        }
    }
}
