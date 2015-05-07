using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Responses
{
    public class GetPaymentOptionsResponse : Response
    {
        public PaymentMethodDetail[] AvailablePaymentMethods { get; set; }

        public GetPaymentOptionsResponse()
        {
            AvailablePaymentMethods = new PaymentMethodDetail[0];
        }
    }
}