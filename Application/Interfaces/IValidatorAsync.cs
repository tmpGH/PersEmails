namespace PersEmails.Application.Interfaces
{
    public interface IValidatorAsync<TCommand>
    {
        Task<bool> IsValid(TCommand command);
    }
}
