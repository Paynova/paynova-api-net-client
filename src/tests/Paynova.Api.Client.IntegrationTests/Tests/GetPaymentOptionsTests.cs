using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    public class GetPaymentOptionsTests : IntegrationTests
    {
        [MyFact]
        public void Can_get_payment_options()
        {
            var response = Client.GetPaymentOptions(new GetPaymentOptionsRequest(
                999.99m,
                PaymentChannelId.Web,
                CurrencyCode.SwedishKrona,
                "SE",
                "SWE"));

            response.ShouldBe();
        }
    }
}