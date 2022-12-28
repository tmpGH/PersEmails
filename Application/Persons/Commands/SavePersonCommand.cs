using PersEmails.Application.Interfaces;
using PersEmails.Application.Persons;
using PersEmails.Domain.Entities;

namespace Application.Persons.Commands
{
    public class SavePersonCommand : ICommandAsync
    {
        private PersonDto person;

        public SavePersonCommand(PersonDto person)
        {
            this.person = person;
        }

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            if (person == null)
                return 0;
            var entity = await context.Persons.FindAsync(person.Id);
            if (entity == null)
                return 0;

            if (!IsPersonValid())
                return 0;

            MapToEntity(person, entity);

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
