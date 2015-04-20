using System;
using System.Net;
using FluentAssertions;
using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    public class FailingClientTests : IntegrationTests<FailingClientTests.Fixture>
    {
        public FailingClientTests() : base(TestStateRecycle.PerTest) { }

        [MyFact]
        public void When_wrong_credentials_It_should_throw_a_web_exception_indicating_unauthorized()
        {
            var fooClient = new PaynovaClient(Client.Connection.ServerAddress, "fake", "fake");

            Action a = () => fooClient.CreateOrder(TestState.CreateOrderRequest);

            a.ShouldThrow<WebException>()
                .And.Response.As<HttpWebResponse>()
                .StatusCode.Should()
                .Be(HttpStatusCode.Unauthorized);
        }

        [MyFact]
        public void When_wrong_server_address_It_should_throw_a_web_exception_indicating_name_resolution_failure()
        {
            var fooClient = new PaynovaClient("http://fdac85aaeadd41ce91e7eacb402034b3.paynova.com", "fake", "fake");

            Action a = () => fooClient.CreateOrder(TestState.CreateOrderRequest);

            a.ShouldThrow<WebException>()
                .And.Status.Should().Be(WebExceptionStatus.NameResolutionFailure);
        }

        [MyFact]
        public void When_invalid_order_It_throws_sdk_exception_with_validation_failure()
        {
            TestState.CreateOrderRequest.AddExtraLineItemWithoutUpdatingTotalAmount();

            Action a = () => Client.CreateOrder(TestState.CreateOrderRequest);

            a.ShouldThrow<PaynovaSdkException>().And.ShouldBe().DueToAnyValidationFailure();
        }

        [MyFact]
        public void When_invalid_payment_initialization_It_throws_sdk_exception_with_validation_failure()
        {
            TestState.CreateOrderResponse = Client.CreateOrder(TestState.CreateOrderRequest);

            TestState.InitializePaymentRequest.AddExtraLineItemWithoutUpdatingTotalAmount();

            Action a = () => Client.InitializePayment(TestState.InitializePaymentRequest);

            a.ShouldThrow<PaynovaSdkException>().And.ShouldBe().DueToAnyValidationFailure();
        }

        [MyFact]
        public void When_getting_a_non_existing_customer_profile_It_should_throw_a_paynova_exception()
        {
            Action a = () => Client.GetCustomerProfile("1fc895e8e99141ecaf52ebd09f6d4a30");

            a.ShouldThrow<PaynovaSdkException>();
        }

        public class Fixture : FlowFixture
        {
        }
    }
}