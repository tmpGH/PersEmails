using System.ComponentModel.DataAnnotations;

namespace PersEmails.Domain.Entities
{
    public class Email
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string EmailAddress { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
