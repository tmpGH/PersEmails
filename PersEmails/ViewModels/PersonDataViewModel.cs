using PersEmails.Application.Emails;
using PersEmails.Application.Persons;

namespace PersEmails.ViewModels
{
    public class PersonDataViewModel
    {
        public PersonWithEmailAddressDto Person { get; set; }
        public IList<EmailDto> Emails { get; set; }
    }
}
