using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Testing.Shoulds;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    public class AnnulInvoiceTests : IntegrationTests<FlowFixture>
    {
        public AnnulInvoiceTests(FlowFixture data, TestStateRecycle testStateRecycle = TestStateRecycle.PerClass) : base(data, testStateRecycle)
        {
            if (_testStateRecycle == TestStateRecycle.PerTest)
                data.Reset();

            TestState = data;
        }

        [Fact, Order(1)]
        public void Look_up_customer()
        {
            TestState.GetAddressesResponse = Client.GetAddresses(TestState.GetAddressesRequest);

            TestState.GetAddressesResponse.ShouldBe().Ok();
        }

        [Fact, Order(2)]
        public void Create_order()
        {
            TestState.CreateOrderResponse = Client.CreateOrder(TestState.CreateOrderRequest);

            TestState.CreateOrderResponse.ShouldBe().Ok();
        }

        [Fact, Order(3)]
        public void Authorize_invoice()
        {
            TestState.AuthorizeInvoiceResponse = Client.AuthorizeInvoice(TestState.AuthorizeInvoiceRequest);

            TestState.AuthorizeInvoiceResponse.ShouldBe().Ok();
        }

        [Fact, Order(4)]
        public void Annul_invoice_authorization()
        {
            Client.AnnulAuthorization(TestState.AnnulAuthorizationRequest);
        }
    }
}