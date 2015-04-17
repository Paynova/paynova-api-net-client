using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class CreateOrderHttpRequestFactoryTests : UnitTestsOf<CreateOrderHttpRequestFactory>
    {
        private const string OrderNumber = "order_123";

        public CreateOrderHttpRequestFactoryTests()
        {
            SUT = new CreateOrderHttpRequestFactory(Runtime, Serializer);
        }

        [MyFact]
        public void When_simple_order_It_generates_relative_url_with_order_number_and_amount_and_currency()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateSimple(OrderNumber);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/create/{0}/{1}/{2}",
                createOrderRequest.OrderNumber,
                createOrderRequest.TotalAmount.ToString(Runtime.NumberFormatProvider),
                createOrderRequest.CurrencyCode);
        }

        [MyFact]
        public void When_detailed_order_It_generates_relative_url_with_order_number_and_amount_and_currency()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateDetailedWithLineItems(OrderNumber, 2);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/create/{0}/{1}/{2}",
                createOrderRequest.OrderNumber,
                createOrderRequest.TotalAmount.ToString(Runtime.NumberFormatProvider),
                createOrderRequest.CurrencyCode);
        }

        [MyFact]
        public void When_simple_order_It_can_create_http_request()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateSimple(OrderNumber);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.SimpleCreateOrder);
        }

        [MyFact]
        public void When_detailed_order_It_can_create_http_request()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateDetailedWithLineItems(OrderNumber, 2);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.DetailedCreateOrder_With_LineItems);
        }

        [MyFact]
        public void When_detailed_order_with_travel_line_items_It_can_create_http_request()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateDetailedWithTravelLineItems(OrderNumber);

            var httpRequest = SUT.Create(createOrderRequest);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.DetailedCreateOrder_With_TravelLineItems);
        }
    }
}