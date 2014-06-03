using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.IntegrationTests.OrderProcessTests
{
    [PrioritizedFixture]
    public class When_simple_order : IntegrationTestsOf<IPaynovaClient>, IUseFixture<When_simple_order.Fixture>
    {
        private Fixture _state;

        public When_simple_order()
        {
            SUT = Client;
        }

        [MyFact(Priority = 1)]
        public void It_should_be_able_to_create_the_order()
        {
            var createOrderRequest = _state.CreateOrderRequest;

            _state.CreateOrderResponse = SUT.CreateOrder(createOrderRequest.OrderNumber, createOrderRequest.CurrencyCode, createOrderRequest.TotalAmount);

            _state.CreateOrderResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 2)]
        public void It_should_be_able_to_initialize_payment()
        {
            _state.InitializePaymentResponse = SUT.InitializePayment(_state.InitializePaymentRequest);

            _state.InitializePaymentResponse.ShouldBe().Ok();
        }

        public void SetFixture(Fixture data)
        {
            _state = data;
        }

        public class Fixture : OrderProcessFixture
        {
            protected override CreateOrderRequest CreateCreateOrderRequest()
            {
                return CreateOrderRequestTestData.CreateSimple(OrderNumber);
            }
        }
    }
}