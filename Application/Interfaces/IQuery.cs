namespace PersEmails.Application.Interfaces
{
    public interface IQuery<TResult>
    {
        TResult Execute(IAppContext context);
    }
}
