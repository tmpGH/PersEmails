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
            var entity = await context.Persons.FindAsync(person.Id);
            if (entity == null)
                return 0;
            // TODO: walidacja pol w PersonDto

            MapToEntity(person, entity);

            return context.SaveChanges();
        }

        private void MapToEntity(PersonDto dto, Person entity)
        {
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            entity.Description = dto.Description;
        } 
    }
}
