using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Emails.Commands
{
    public class DeleteEmailCommand : ICommand
    {
        private int emailId;

        public DeleteEmailCommand(int emailId)
        {
            this.emailId = emailId;
        }

        public int Execute(IAppContext context)
        {
            var entity = context.Emails.Find(emailId);

            context.Emails.Remove(entity);
            
            return context.SaveChanges();
        }
    }
}
