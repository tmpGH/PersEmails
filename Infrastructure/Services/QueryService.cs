using PersEmails.Application.Interfaces;
using PersEmails.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PersEmails.Infrastructure.Services
{
    public class QueryService : IQueryService
    {
        private IServiceProvider _serviceProvider;

        public QueryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var dbContext = _serviceProvider.GetService<IAppContext>();
            return query.Execute(dbContext);
        }

        public Task<TResult> ExecuteAsync<TResult>(IQueryAsync<TResult> query, CancellationToken cancellationToken = default)
        {
            var dbContext = _serviceProvider.GetService<IAppContext>();
            return query.ExecuteAsync(dbContext, cancellationToken);
        }
    }
}
