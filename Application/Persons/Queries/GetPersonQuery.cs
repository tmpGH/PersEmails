using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using System.Data;

namespace PersEmails.Application.Persons.Queries
{
    public class GetPersonQuery : IQuery<PersonDto>
    {
        private int personId;

        public GetPersonQuery(int personId)
        {
            this.personId = personId;
        }

        public PersonDto Execute(IAppContext context)
        {
            var person = (
                from p in context.Persons
                where p.Id == personId
                select MapToDto(p)
            ).FirstOrDefault();

            return person;
        }

        private static PersonDto MapToDto(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Description = person.Description
            };
        }
    }
}
