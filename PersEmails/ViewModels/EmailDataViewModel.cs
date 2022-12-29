using System.ComponentModel.DataAnnotations;

namespace PersEmails.ViewModels
{
    public class EmailDataViewModel
    {
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
    }
}
