namespace PersEmails.Application.Interfaces
{
    public interface IQueryAsync<TResult>
    {
        Task<TResult> ExecuteAsync(IAppContext context, CancellationToken cancellationToken);
    }
}
