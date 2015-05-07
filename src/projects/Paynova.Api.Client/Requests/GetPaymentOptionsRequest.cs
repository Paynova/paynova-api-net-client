using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    public class GetPaymentOptionsRequest : Request
    {
        public decimal TotalAmount { get; private set; }
        public int PaymentChannelId { get; private set; }
        public string CurrencyCode { get; private set; }
        public string CountryCode { get; private set; }
        public string LanguageCode { get; private set; }

        public GetPaymentOptionsRequest(decimal totalAmount, PaymentChannelId paymentChannelId, CurrencyCode currencyCode, string countryCode, string languageCode)
        {
            Ensure.That(totalAmount, "totalAmount").IsGt(0);
            Ensure.That(currencyCode, "currencyCode").IsNotNull();
            Ensure.That(countryCode, "countryCode").IsNotNullOrEmpty();
            Ensure.That(languageCode, "languageCode").IsNotNullOrEmpty();

            TotalAmount = totalAmount;
            PaymentChannelId = paymentChannelId;
            CurrencyCode = currencyCode.Alpha3;
            CountryCode = countryCode;
            LanguageCode = languageCode;
        }
    }
}