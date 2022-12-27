namespace PersEmails.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? Error { get; set; }
        public bool ShowError => !string.IsNullOrEmpty(Error);
    }
}