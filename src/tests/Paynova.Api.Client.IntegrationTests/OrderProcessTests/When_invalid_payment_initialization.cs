using System;
using FluentAssertions;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.IntegrationTests.OrderProcessTests
{
    [PrioritizedFixture]
    public class When_invalid_payment_initialization : IntegrationTestsOf<IPaynovaClient>, IUseFixture<When_invalid_payment_initialization.Fixture>
    {
        private Fixture _state;

        public When_invalid_payment_initialization()
        {
            SUT = Client;
        }

        [MyFact(Priority = 1)]
        public void It_can_create_the_order()
        {
            _state.CreateOrderResponse = SUT.CreateOrder(_state.CreateOrderRequest);
        }

        [MyFact(Priority = 2)]
        public void It_throws_sdk_exception_with_validation_failure()
        {
            Action a = () => SUT.InitializePayment(_state.InitializePaymentRequest);

            a.ShouldThrow<PaynovaSdkException>().And.ShouldBe().DueToAnyValidationFailure();
        }

        public void SetFixture(Fixture data)
        {
            _state = data;
        }

        public class Fixture : OrderProcessFixture
        {
            protected override CreateOrderRequest CreateCreateOrderRequest()
            {
                return CreateOrderRequestTestData.CreateDetailedWithLineItems(OrderNumber, 2);
            }

            protected override InitializePaymentRequest CreateInitializePaymentRequest()
            {
                return InitializePaymentRequestTestData.CreateDetailedWithInvalidTotalAmount(CreateOrderResponse.OrderId);
            }
        }
    }
}