using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersEmails.Application.Emails.Queries
{
    public class GetPersonEmailsQuery : IQueryAsync<PersonEmailListDto>
    {
        private int personId;

        public GetPersonEmailsQuery(int personId)
        {
            this.personId = personId;
        }

        public async Task<PersonEmailListDto> ExecuteAsync(IAppContext context)
        {
            var emails = await (
                from e in context.Emails
                where e.PersonId == personId
                select Map(e)
            ).ToListAsync();

            return new PersonEmailListDto(emails);
        }

        private static EmailDto Map(Email e)
        {
            return new EmailDto
            {
                Id = e.Id,
                EmailAddress = e.EmailAddress,
                PersonId = e.PersonId
            };
        }

    }
}
