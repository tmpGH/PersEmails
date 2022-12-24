namespace PersEmails.Application.Emails
{
    public class PersonEmailListDto
    {
        public PersonEmailListDto(IList<EmailDto> emails)
        {
            Emails = emails ?? new List<EmailDto>();
        }

        public int PersonId { get; set; }

        public IList<EmailDto> Emails { get; private set; }
    }
}
