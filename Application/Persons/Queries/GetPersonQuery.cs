using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PersEmails.Application.Persons.Queries
{
    public class GetPersonQuery : IQueryAsync<PersonDto>
    {
        private int personId;

        public GetPersonQuery(int personId)
        {
            this.personId = personId;
        }

        public async Task<PersonDto> ExecuteAsync(IAppContext context)
        {
            var person = await (
                from p in context.Persons
                where p.Id == personId
                select Map(p)
            ).FirstOrDefaultAsync();

            return person;
        }

        private static PersonDto Map(Person p)
        {
            return new PersonDto
            {
                Id = p.Id,
                Name = p.Name,
                Surname = p.Surname,
                Description = p.Description
            };
        }
    }
}
