using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;
using System.Data;

namespace PersEmails.Application.Persons.Queries
{
    public class GetPersonQuery : IQuery<PersonWithEmailAddressDto>
    {
        private int personId;

        public GetPersonQuery(int personId)
        {
            this.personId = personId;
        }

        public PersonWithEmailAddressDto Execute(IAppContext context)
        {
            var person = (
                from p in context.Persons
                where p.Id == personId
                select MapToDto(p)
            ).FirstOrDefault();

            return person;
        }

        private static PersonWithEmailAddressDto MapToDto(Person person)
        {
            return new PersonWithEmailAddressDto
            {
                Id = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Description = person.Description
            };
        }
    }
}
