namespace PersEmails.Application.Interfaces
{
    public interface ICommand
    {
        int Execute(IAppContext context);
    }
}
