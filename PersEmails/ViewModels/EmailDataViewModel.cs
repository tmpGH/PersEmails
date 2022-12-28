using PersEmails.Application.Emails;
using PersEmails.Application.Persons;

namespace PersEmails.ViewModels
{
    public class EmailDataViewModel
    {
        public EmailDto Email { get; set; }
        public PersonWithEmailAddressDto Person { get; set; }
    }
}
