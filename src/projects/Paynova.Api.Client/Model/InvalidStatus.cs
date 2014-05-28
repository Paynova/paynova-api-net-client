namespace Paynova.Api.Client.Model
{
    public class InvalidStatus
    {
        public int ErrorNumber { get; set; }
        public string StatusKey { get; set; }
        public string StatusMessage { get; set; }
        public Error[] Errors { get; set; }

        public InvalidStatus()
        {
            Errors = new Error[0];
        }
    }
}