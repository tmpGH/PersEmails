namespace PersEmails.Application.Emails
{
    public class EmailWithNamesListDto
    {
        public EmailWithNamesListDto(IList<EmailWithNamesDto> emails)
        {
            Emails = emails ?? new List<EmailWithNamesDto>();
        }

        public IList<EmailWithNamesDto> Emails { get; private set; }
    }
}
