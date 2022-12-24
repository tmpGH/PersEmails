using PersEmails.Application.Interfaces;
using PersEmails.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PersEmails.Infrastructure.Services
{
    public class QueryService : IQueryService
    {
        private IServiceProvider serviceProvider;

        public QueryService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public TResult Execute<TResult>(IQuery<TResult> query)
        {
            var dbContext = serviceProvider.GetService<IAppContext>();
            return query.Execute(dbContext);
        }

        public Task<TResult> ExecuteAsync<TResult>(IQueryAsync<TResult> query, CancellationToken cancellationToken = default)
        {
            var dbContext = serviceProvider.GetService<IAppContext>();
            return query.ExecuteAsync(dbContext);
        }
    }
}
