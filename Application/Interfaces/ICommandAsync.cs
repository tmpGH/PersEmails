namespace PersEmails.Application.Interfaces
{
    public interface ICommandAsync
    {
        Task<int> ExecuteAsync(IAppContext context);
    }
}
