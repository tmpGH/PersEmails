using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Emails.Queries
{
    public class GetAllEmailsQuery : IQuery<IList<EmailWithNamesDto>>
    {
        public IList<EmailWithNamesDto> Execute(IAppContext context)
        {
            var emails = (
                from e in context.Emails
                from p in context.Persons
                where e.PersonId == p.Id
                orderby e.EmailAddress
                select MapToDto(e, p)
            ).ToList();

            return emails;
        }

        private static EmailWithNamesDto MapToDto(Email email, Person person)
        {
            return new EmailWithNamesDto
            {
                Id = email.Id,
                EmailAddress = email.EmailAddress,
                Name = person?.Name,
                Surname = person?.Surname
            };
        }
    }
}
