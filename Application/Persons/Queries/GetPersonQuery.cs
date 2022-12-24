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
                select Map(p)
            ).FirstOrDefault();

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
