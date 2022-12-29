using PersEmails.Application.Interfaces;

namespace PersEmails.Application.Persons.Commands
{
    public class SavePersonCommand : ICommandAsync
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }

        public async Task<int> ExecuteAsync(IAppContext context, CancellationToken cancellationToken)
        {
            var entity = await context.Persons.FindAsync(Id);
            if (entity == null)
                return 0;

            entity.Name = Name;
            entity.Surname = Surname;
            entity.Description = Description;

            return context.SaveChanges();
        }
    }
}
