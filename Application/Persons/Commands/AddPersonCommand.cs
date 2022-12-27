using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons;
using PersEmails.Domain.Entities;

namespace Application.Persons.Commands
{
    public class AddPersonCommand : ICommand
    {
        private PersonDto person;

        public AddPersonCommand(PersonDto person)
        {
            this.person = person;
        }

        public int Execute(IAppContext context)
        {
            if (person == null)
                return 0;
            // TODO: walidacja pol w PersonDto

            context.Persons.Add(MapToEntity(person));

            return context.SaveChanges();
        }

        private Person MapToEntity(PersonDto person)
        {
            return new Person
            {
                Name = person.Name,
                Surname = person.Surname,
                Description = person.Description
            };
        }
    }
}
