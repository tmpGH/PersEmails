using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PersEmails.Application.Persons.Queries
{
    public class GetPersonsQuery : IQueryAsync<IList<PersonDto>>
    {
        public async Task<IList<PersonDto>> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            var persons = await (
                from p in context.Persons
                from e in context.Emails.Where(ee => p.Id == ee.PersonId)
                                        .Take(1)
                                        .DefaultIfEmpty()
                orderby p.Surname, p.Name
                select MapToDto(p, e.EmailAddress)
            ).ToListAsync();

            return persons;
        }

        private static PersonDto MapToDto(Person person, string emailAddress)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Description = person.Description,
                EmailAddress = emailAddress
            };
        }
    }
}
