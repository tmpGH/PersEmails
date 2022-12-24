using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Interfaces
{
    public interface IQueryService
    {
        TResult Execute<TResult>(IQuery<TResult> query);
        Task<TResult> ExecuteAsync<TResult>(IQueryAsync<TResult> query, CancellationToken cancellationToken = default);
    }
}
