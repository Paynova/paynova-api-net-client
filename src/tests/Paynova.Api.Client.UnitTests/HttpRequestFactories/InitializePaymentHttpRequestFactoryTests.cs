using System;
using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class InitializePaymentHttpRequestFactoryTests : UnitTestsOf<InitializePaymentHttpRequestFactory>
    {
        private readonly Guid _orderId = new Guid("a83aec2a-f9fd-4193-978a-83b3e474769d");

        public InitializePaymentHttpRequestFactoryTests()
        {
            SUT = new InitializePaymentHttpRequestFactory(Runtime, Serializer);
        }

        [Fact]
        public void When_simple_init_payment_It_generates_relative_url_with_order_id()
        {
            var initializePaymentRequest = InitializePaymentRequestTestData.CreateSimple(_orderId);

            var httpRequest = SUT.Create(initializePaymentRequest);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/{0}/initializePayment",
                _orderId.ToString("n"));
        }

        [Fact]
        public void When_detailed_init_payment_It_generates_relative_url_with_order_id()
        {
            var initializePaymentRequest = InitializePaymentRequestTestData.CreateDetailedWithLineItems(_orderId, 2);

            var httpRequest = SUT.Create(initializePaymentRequest);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/{0}/initializePayment",
                _orderId.ToString("n"));
        }

        [Fact]
        public void When_simple_init_payment_It_can_create_http_request()
        {
            var initializePaymentRequest = InitializePaymentRequestTestData.CreateSimple(_orderId);

            var httpRequest = SUT.Create(initializePaymentRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.SimpleInitPayment);
        }

        [Fact]
        public void When_detailed_init_payment_It_can_create_http_request()
        {
            var createOrderRequest = InitializePaymentRequestTestData.CreateDetailedWithLineItems(_orderId, 2);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.DetailedInitPaymentRequest_With_LineItems);
        }

        [Fact]
        public void When_detailed_init_payment_with_travel_line_items_It_can_create_http_request()
        {
            var createOrderRequest = InitializePaymentRequestTestData.CreateDetailedWithTravelLineItems(_orderId);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.DetailedInitPaymentRequest_With_TravelLineItems);
        }
    }
}