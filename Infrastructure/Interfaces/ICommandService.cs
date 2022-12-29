using PersEmails.Application.Interfaces;

namespace PersEmails.Infrastructure.Interfaces
{
    public interface ICommandService
    {
        int Execute(ICommand command);
        Task<int> ExecuteAsync(ICommandAsync command, CancellationToken cancellationToken = default);
    }
}
