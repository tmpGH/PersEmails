using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PersEmails.Application.Emails;

namespace PersEmails.Application.Emails.Queries
{
    public class GetAllEmailsQuery : IQueryAsync<EmailWithNamesListDto>
    {
        public async Task<EmailWithNamesListDto> ExecuteAsync(IAppContext context)
        {
            var emails = await (
                from e in context.Emails
                from p in context.Persons
                where e.PersonId == p.Id
                orderby e.EmailAddress
                select Map(e, p)
            ).ToListAsync();

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
