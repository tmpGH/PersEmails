using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PersEmails.Application.Persons.Queries
{
    public class GetPersonsQuery : IQueryAsync<PersonListDto>
    {
        public async Task<PersonListDto> ExecuteAsync(IAppContext context)
        {
            var persons = await (
                from p in context.Persons
                from e in context.Emails.Where(ee => p.Id == ee.PersonId)
                                        .Take(1)
                                        .DefaultIfEmpty()
                orderby p.Surname, p.Name
                select Map(p, e)
            ).ToListAsync();

            return new PersonListDto(persons);
        }

        private static PersonDto Map(Person p, Email e)
        {
            return new PersonDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Description = p.Description,
                EmailAddress = e?.EmailAddress
            };
        }
    }
}
