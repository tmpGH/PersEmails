using System.ComponentModel.DataAnnotations;

namespace PersEmails.Domain.Entities
{
    public class Person
    {
        public Person()
        {
            this.Emails = new List<Email>();
        }

        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        public string Description { get; set; }

        public List<Email> Emails { get; private set; }
    }
}
