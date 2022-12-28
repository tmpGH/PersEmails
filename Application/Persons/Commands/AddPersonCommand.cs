using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Persons.Commands
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
            if (!IsPersonValid())
                return 0;

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

        private bool IsPersonValid()
        {
            if (person == null)
                return false;
            if (string.IsNullOrWhiteSpace(person.Name))
                return false;
            if (string.IsNullOrWhiteSpace(person.Surname))
                return false;
            if (person.Name.Length > 50)
                return false;
            if (person.Surname.Length > 50)
                return false;

            return true;
        }
    }
}
