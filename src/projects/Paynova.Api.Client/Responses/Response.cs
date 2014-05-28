using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Responses
{
    public abstract class Response
    {
        public SuccessfulStatus Status { get; set; }

        protected Response()
        {
            Status = new SuccessfulStatus();
        }
    }
}