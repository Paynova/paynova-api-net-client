using System;
using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.Shoulds;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class FinalizeAuthorizationHttpRequestFactoryTests : UnitTestsOf<FinalizeAuthorizationHttpRequestFactory>
    {
        private const decimal TotalAmount = 112.75m;
        private const string TransactionId = "transaction1";
        private readonly string _totalAmountString;
        private readonly Guid _orderId ;

        public FinalizeAuthorizationHttpRequestFactoryTests()
        {
            _totalAmountString = TotalAmount.ToString(Runtime.NumberFormatProvider);
            _orderId = new Guid("7a246c7b-d97f-461d-9da8-e6f710f319a1");

            SUT = new FinalizeAuthorizationHttpRequestFactory(Runtime, Serializer);
        }

        [Fact]
        public void When_order_id_is_not_specified_It_should_render_relative_url_with_transaction_id_and_amount()
        {
            var request = new FinalizeAuthorizationRequest(TransactionId, TotalAmount);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostAgainst(
                "/transactions/{0}/finalize/{1}",
                TransactionId,
                _totalAmountString);
        }

        [Fact]
        public void When_order_id_is_specified_It_should_render_relative_url_with_order_id_and_transaction_id_and_amount()
        {
            var request = new FinalizeAuthorizationRequest(TransactionId, _orderId, TotalAmount);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/{0}/transactions/{1}/finalize/{2}",
                _orderId.ToString("n"),
                TransactionId,
                _totalAmountString);
        }

        [Fact]
        public void When_simple_finalize_auth_It_can_create_http_request()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.SimpleFinalizeAuth);
        }

        [Fact]
        public void When_detailed_finalize_auth_It_can_create_http_request()
        {
            var request = CreateRequest().WithLineItems(LineItemTestData.CreateLineItems(2));

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.FinalizeAuth_With_LineItems);
        }

        [Fact]
        public void When_detailed_finalize_auth_with_travel_line_items_It_can_create_http_request()
        {
            var request = CreateRequest().WithLineItems(LineItemTestData.CreateTravelLineItems());

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.FinalizeAuth_With_TravelLineItems);
        }

        private FinalizeAuthorizationRequest CreateRequest()
        {
            return new FinalizeAuthorizationRequest(TransactionId, _orderId, TotalAmount)
            {
                InvoiceId = "invoice1"
            };
        }
    }
}