using Microsoft.Extensions.DependencyInjection;
using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Interfaces
{
    public interface ICommandService
    {
        TCommand GetCommand<TCommand>() where TCommand : ICommand;
        TCommand GetAsyncCommand<TCommand>() where TCommand : ICommandAsync;

        int Execute(ICommand command);
        Task<int> ExecuteAsync(ICommandAsync command, CancellationToken cancellationToken = default);
    }
}
