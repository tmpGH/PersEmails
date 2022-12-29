using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Emails.Commands
{
    public class DeleteEmailCommand : ICommand
    {
        public int Id { get; set; }

        public int Execute(IAppContext context)
        {
            var entity = context.Emails.Find(Id);
            if(entity == null)
                return 0;
            
            context.Emails.Remove(entity);
            
            return context.SaveChanges();
        }
    }
}
