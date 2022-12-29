namespace PersEmails.Application.Interfaces
{
    public interface IValidator<TCommand>
    {
        bool IsValid(TCommand command);
    }
}
