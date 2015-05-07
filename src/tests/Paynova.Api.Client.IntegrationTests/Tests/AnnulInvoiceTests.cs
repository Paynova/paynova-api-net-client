using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    [PrioritizedFixture]
    public class AnnulInvoiceTests : IntegrationTests<FlowFixture>
    {
        [MyFact(Priority = 1)]
        public void Look_up_customer()
        {
            TestState.GetAddressesResponse = Client.GetAddresses(TestState.GetAddressesRequest);

            TestState.GetAddressesResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 2)]
        public void Create_order()
        {
            TestState.CreateOrderResponse = Client.CreateOrder(TestState.CreateOrderRequest);

            TestState.CreateOrderResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 3)]
        public void Authorize_invoice()
        {
            TestState.AuthorizeInvoiceResponse = Client.AuthorizeInvoice(TestState.AuthorizeInvoiceRequest);

            TestState.AuthorizeInvoiceResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 4)]
        public void Annul_invoice_authorization()
        {
            Client.AnnulAuthorization(TestState.AnnulAuthorizationRequest);
        }
    }
}