using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Persons.Commands
{
    public class AddPersonCommand : ICommand
    {
        private readonly ILogger<AddPersonCommand> logger;

        public PersonDto Person { get; set; }

        public AddPersonCommand(ILogger<AddPersonCommand> logger) => this.logger = logger;

        public int Execute(IAppContext context)
        {
            if (!IsPersonValid())
                return 0;

            context.Persons.Add(MapToEntity(Person));

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
            if (Person == null)
            {
                logger.Log(LogLevel.Error, "Empty object: Person");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Person.Name))
            {
                logger.Log(LogLevel.Error, "Empty field: Name");
                return false;
            }
            if (string.IsNullOrWhiteSpace(Person.Surname))
            {
                logger.Log(LogLevel.Error, "Empty field: Surname");
                return false;
            }
            if (Person.Name.Length > 50)
            {
                logger.Log(LogLevel.Error, "Too long field: Name");
                return false;
            }
            if (Person.Surname.Length > 50)
            {
                logger.Log(LogLevel.Error, "Too long field: Surname");
                return false;
            }

            return true;
        }
    }
}
