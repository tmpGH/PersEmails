using System.ComponentModel.DataAnnotations;

namespace PersEmails.Application.Emails
{
    public class EmailDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        public int PersonId { get; set; }
    }
}
