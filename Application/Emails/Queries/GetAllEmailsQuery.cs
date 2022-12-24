using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Emails.Queries
{
    public class GetAllEmailsQuery : IQuery<EmailWithNamesListDto>
    {
        public EmailWithNamesListDto Execute(IAppContext context)
        {
            var emails = (
                from e in context.Emails
                from p in context.Persons
                where e.PersonId == p.Id
                orderby e.EmailAddress
                select Map(e, p)
            ).ToList();

            return new EmailWithNamesListDto(emails);
        }

        private static EmailWithNamesDto Map(Email e, Person p)
        {
            return new EmailWithNamesDto
            {
                Id = e.Id,
                EmailAddress = e.EmailAddress,
                Name = p?.Name,
                Surname = p?.Surname
            };
        }
    }
}
