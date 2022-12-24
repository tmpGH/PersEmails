using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Interfaces
{
    public interface IQuery<TResult>
    {
        TResult Execute(IAppContext context);
    }
}
