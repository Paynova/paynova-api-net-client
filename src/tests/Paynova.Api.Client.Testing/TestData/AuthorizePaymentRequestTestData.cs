using System;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;

namespace Paynova.Api.Client.Testing.TestData
{
    public static class AuthorizePaymentRequestTestData
    {
        public static AuthorizeInvoiceRequest CreateForAuthorizationOfInvoiceRequest(Guid orderId, decimal totalAmount, string paymentMethodProductId = "DirectInvoice")
        {
            return new AuthorizeInvoiceRequest(orderId, totalAmount, PaymentMethod.PaynovaInvoice, paymentMethodProductId, PaymentChannelId.Web);
        }
    }
}