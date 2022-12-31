using System.ComponentModel.DataAnnotations;

namespace PersEmails.ViewModels.Emails
{
    public class EmailDataViewModel
    {
        [StringLength(50)]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
    }
}
