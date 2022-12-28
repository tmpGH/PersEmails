using System.ComponentModel.DataAnnotations;

namespace PersEmails.ViewModels
{
    public class EditPersonViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        public string? Description { get; set; }
    }
}
