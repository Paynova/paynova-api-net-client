namespace Paynova.Api.Client.Model
{
    public class SuccessfulStatus
    {
        public string StatusKey { get; set; }
        public string StatusMessage { get; set; }

        public SuccessfulStatus()
        {
            StatusKey = "SUCCESS";
            StatusMessage = "The operation was successful.";
        }
    }
}