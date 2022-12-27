using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PersEmails.Application.Emails.Queries
{
    public class GetPersonEmailsQuery : IQueryAsync<IList<EmailDto>>
    {
        private int personId;

        public GetPersonEmailsQuery(int personId)
        {
            this.personId = personId;
        }

        public async Task<IList<EmailDto>> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            var emails = await (
                from e in context.Emails
                where e.PersonId == personId
                select MapToDto(e)
            ).ToListAsync();

            return emails;
        }

        private static EmailDto MapToDto(Email email)
        {
            return new EmailDto
            {
                Id = email.Id,
                EmailAddress = email.EmailAddress,
                PersonId = email.PersonId
            };
        }
    }
}
