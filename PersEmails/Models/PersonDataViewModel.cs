using PersEmails.Application.Emails;
using PersEmails.Application.Persons;

namespace PersEmails.Models
{
    public class PersonDataViewModel
    {
        public PersonDto Person { get; set; }
        public IList<EmailDto> Emails { get; set; }
    }
}
