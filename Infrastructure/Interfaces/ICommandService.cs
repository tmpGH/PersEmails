using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Interfaces
{
    public interface ICommandService
    {
        int Execute<TCommand>(TCommand command) where TCommand : ICommand;
        Task<int> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommandAsync;
    }
}
