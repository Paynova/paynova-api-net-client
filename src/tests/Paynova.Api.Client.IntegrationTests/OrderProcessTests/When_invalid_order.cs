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
    public class When_invalid_order : IntegrationTestsOf<IPaynovaClient>, IUseFixture<When_invalid_order.Fixture>
    {
        private Fixture _state;

        public When_invalid_order()
        {
            SUT = Client;
        }

        [MyFact]
        public void It_throws_sdk_exception_with_validation_failure()
        {
            Action a = () => SUT.CreateOrder(_state.CreateOrderRequest);

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
                return CreateOrderRequestTestData.CreateDetailedWithInvalidTotalAmount(OrderNumber);
            }
        }
    }
}