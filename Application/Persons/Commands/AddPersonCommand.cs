using PersEmails.Application.Interfaces;
using PersEmails.Domain.Entities;

namespace PersEmails.Application.Persons.Commands
{
    public class AddPersonCommand : ICommand
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }

        public int Execute(IAppContext context)
        {
            context.Persons.Add(new Person
            {
                Name = this.Name,
                Surname = this.Surname,
                Description = this.Description
            });

            return context.SaveChanges();
        }
    }
}
