using PersEmails.Application.Emails;
using PersEmails.Application.Persons;

namespace PersEmails.ViewModels
{
    public class EmailDataViewModel
    {
        public EmailDto Email { get; set; }
        public PersonDto Person { get; set; }
    }
}
