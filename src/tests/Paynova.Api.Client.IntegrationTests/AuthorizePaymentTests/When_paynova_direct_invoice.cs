using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.IntegrationTests.AuthorizePaymentTests
{
    [PrioritizedFixture]
    public class When_authorize_paynova_direct_invoice : IntegrationTestsOf<IPaynovaClient>, IUseFixture<When_authorize_paynova_direct_invoice.Fixture>
    {
        private Fixture _state;

        public When_authorize_paynova_direct_invoice()
        {
            SUT = Client;
        }

        [MyFact(Priority = 1)]
        public void It_should_be_able_to_create_the_order()
        {
            _state.CreateOrderResponse = SUT.CreateOrder(_state.CreateOrderRequest);

            _state.CreateOrderResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 2)]
        public void It_should_be_able_to_authorize_the_invoice()
        {
            _state.AuthorizeInvoiceResponse = SUT.AuthorizeInvoice(_state.AuthorizeInvoiceRequest);

            _state.AuthorizeInvoiceResponse.ShouldBe().Ok();
        }

        public void SetFixture(Fixture data)
        {
            _state = data;
        }

        public class Fixture : AuthorizeInvoiceFixture
        {
            protected override CreateOrderRequest CreateCreateOrderRequest()
            {
                return CreateOrderRequestTestData.CreateDetailedWithLineItems(OrderNumber, 2);
            }

            protected override AuthorizeInvoiceRequest CreateAuthorizeInvoiceRequest()
            {
                return AuthorizePaymentRequestTestData.CreateForAuthorizationOfInvoiceRequest(CreateOrderResponse.OrderId, CreateOrderRequest.TotalAmount);
            }
        }
    }
}