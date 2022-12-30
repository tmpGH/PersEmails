using PersEmails.Application.Persons.Queries;

namespace PersEmails.ViewModels.Persons
{
    public class PersonListViewModel : PageableViewModel
    {
        public IList<PersonWithEmailAddressDto> Persons { get; set; }
    }
}
