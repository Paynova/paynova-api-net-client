using System;
using System.Net;
using FluentAssertions;
using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Testing.Shoulds;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    public class FailingClientTests : IntegrationTests<FlowFixture>
    {
        public FailingClientTests(FlowFixture data) : base(data, TestStateRecycle.PerClass)
        {
            if (_testStateRecycle == TestStateRecycle.PerTest)
                data.Reset();

            TestState = data;
        }


        [Fact, Order(2)]
        public void When_wrong_credentials_It_should_throw_a_web_exception_indicating_unauthorized()
        {
            var fooClient = new PaynovaClient(Client.Connection.ServerAddress, "fake", "fake");

            Action a = () => fooClient.CreateOrder(TestState.CreateOrderRequest);

            a.Should().Throw<WebException>()
                .And.Response.As<HttpWebResponse>()
                .StatusCode.Should()
                .Be(HttpStatusCode.Unauthorized);
        }

        [Fact, Order(2)]
        public void When_wrong_server_address_It_should_throw_a_web_exception_indicating_name_resolution_failure()
        {
            var fooClient = new PaynovaClient("http://fdac85aaeadd41ce91e7eacb402034b3.paynova.com", "fake", "fake");

            Action a = () => fooClient.CreateOrder(TestState.CreateOrderRequest);

            a.Should().Throw<WebException>()
                .And.Status.Should().Be(WebExceptionStatus.NameResolutionFailure);
        }

        [Fact, Order(2)]
        public void When_invalid_order_It_throws_sdk_exception_with_validation_failure()
        {
            TestState.CreateOrderRequest.AddExtraLineItemWithoutUpdatingTotalAmount();

            Action a = () => Client.CreateOrder(TestState.CreateOrderRequest);

            a.Should().Throw<PaynovaSdkException>().And.ShouldBe().DueToAnyValidationFailure();
        }

        [Fact, Order(1)]
        public void When_invalid_payment_initialization_It_throws_sdk_exception_with_validation_failure()
        {
            TestState.CreateOrderResponse = Client.CreateOrder(TestState.CreateOrderRequest);

            TestState.InitializePaymentRequest.AddExtraLineItemWithoutUpdatingTotalAmount();

            Action a = () => Client.InitializePayment(TestState.InitializePaymentRequest);

            a.Should().Throw<PaynovaSdkException>().And.ShouldBe().DueToAnyValidationFailure();
        }

        [Fact, Order(2)]
        public void When_getting_a_non_existing_customer_profile_It_should_throw_a_paynova_exception()
        {
            Action a = () => Client.GetCustomerProfile("1fc895e8e99141ecaf52ebd09f6d4a30");

            a.Should().Throw<PaynovaSdkException>();
        }
    }
}