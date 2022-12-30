using PersEmails.Application.Emails.Queries;

namespace PersEmails.ViewModels.Emails
{
    public class EmailListViewModel : PageableViewModel
    {
        public IList<EmailWithNamesDto> Emails { get; set; }
    }
}
