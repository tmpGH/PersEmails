using PersEmails.Application.Emails;

namespace PersEmails.ViewModels
{
    public class EmailDataViewModel
    {
        public EmailDto Email { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
    }
}
