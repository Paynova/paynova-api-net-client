using Paynova.Api.Client.IntegrationTests.Fixtures;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.IntegrationTests.Tests
{
    [PrioritizedFixture]
    public class InvoiceTests : IntegrationTests<FlowFixture>
    {
        [MyFact(Priority = 1)]
        public void Look_up_customer()
        {
            TestState.GetAddressesResponse = Client.GetAddresses(TestState.GetAddressesRequest);

            TestState.GetAddressesResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 2)]
        public void Create_order()
        {
            TestState.CreateOrderResponse = Client.CreateOrder(TestState.CreateOrderRequest);

            TestState.CreateOrderResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 3)]
        public void Authorize_invoice()
        {
            TestState.AuthorizeInvoiceResponse = Client.AuthorizeInvoice(TestState.AuthorizeInvoiceRequest);

            TestState.AuthorizeInvoiceResponse.ShouldBe().Ok();
        }

        [MyFact(Priority = 4)]
        public void Finalize_invoice_authorization()
        {
            TestState.FinalizeAuthorizationResponse = Client.FinalizeAuthorization(TestState.FinalizeAuthorizationRequest);

            TestState.FinalizeAuthorizationResponse.ShouldBe().AuthorizedInFull(TestState.AuthorizeInvoiceRequest.TotalAmount);
        }

        [MyFact(Priority = 5)]
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

        [MyFact(Priority = 6)]
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