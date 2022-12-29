namespace PersEmails.Application.Persons.Queries
{
    public class PersonWithEmailAddressDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string EmailAddress { get; set; }
    }
}
