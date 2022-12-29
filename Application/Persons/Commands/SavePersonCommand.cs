using Microsoft.Extensions.Logging;
using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Persons.Commands
{
    public class SavePersonCommand : ICommandAsync
    {
        private readonly ILogger<SavePersonCommand> logger;

        public PersonDto Person { get; set; }

        public SavePersonCommand(ILogger<SavePersonCommand> logger) => this.logger = logger;

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            if (Person == null)
            {
                logger.Log(LogLevel.Error, "Empty object: Person");
                return 0;
            }
            var entity = await context.Persons.FindAsync(Person.Id);
            if (entity == null)
            {
                logger.Log(LogLevel.Error, $"Person with id {Person.Id} not found");
                return 0;
            }

            if (!IsPersonValid())
                return 0;

            MapToEntity(Person, entity);

            return context.SaveChanges();
        }

        private void MapToEntity(PersonDto dto, Person entity)
        {
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            entity.Description = dto.Description;
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
