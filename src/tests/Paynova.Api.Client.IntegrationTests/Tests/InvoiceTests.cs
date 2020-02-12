using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.Shoulds;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Paynova.Api.Client.IntegrationTests.Tests
{    
    public class InvoiceTests : IntegrationTests<FlowFixture>
    {
        public InvoiceTests(FlowFixture data, TestStateRecycle testStateRecycle = TestStateRecycle.PerClass) : base(data, testStateRecycle)
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
        public void Finalize_invoice_authorization()
        {
            TestState.FinalizeAuthorizationResponse = Client.FinalizeAuthorization(TestState.FinalizeAuthorizationRequest);

            TestState.FinalizeAuthorizationResponse.ShouldBe().AuthorizedInFull(TestState.AuthorizeInvoiceRequest.TotalAmount);
        }

        [Fact, Order(5)]
        public void Refund_first_line()
        {
            var refundLine1Request = new RefundPaymentRequest(
                TestState.FinalizeAuthorizationResponse.TransactionId,
                TestState.CreateOrderResponse.OrderId,
                TestState.CreateOrderRequest.LineItems[0].TotalLineAmount)
                .AddLineItem(TestState.CreateOrderRequest.LineItems[0]);

            var refundLine1Response = Client.RefundPayment(refundLine1Request);
            refundLine1Response.ShouldBe().PartiallyRefunded(refundLine1Request.TotalAmount);
        }

        [Fact, Order(6)]
        public void Refund_remaining_two_lines()
        {
            var refundRemainingRequest = new RefundPaymentRequest(
                TestState.FinalizeAuthorizationResponse.TransactionId,
                TestState.CreateOrderResponse.OrderId,
                TestState.CreateOrderRequest.LineItems[1].TotalLineAmount +
                TestState.CreateOrderRequest.LineItems[2].TotalLineAmount)
                .AddLineItem(TestState.CreateOrderRequest.LineItems[1])
                .AddLineItem(TestState.CreateOrderRequest.LineItems[2]);

            var refundRemainingResponse = Client.RefundPayment(refundRemainingRequest);
            refundRemainingResponse.ShouldBe().RefundedInFull(TestState.CreateOrderRequest.TotalAmount);
        }
    }
}