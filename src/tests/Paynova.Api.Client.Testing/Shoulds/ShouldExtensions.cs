using System.Diagnostics;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public static class ShouldExtensions
    {
        [DebuggerStepThrough]
        public static HttpRequestShouldBe ShouldBe(this HttpRequest request)
        {
            return new HttpRequestShouldBe(request);
        }

        [DebuggerStepThrough]
        public static CreateOrderResponseShouldBe ShouldBe(this CreateOrderResponse response)
        {
            return new CreateOrderResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static AuthorizeInvoiceResponseShouldBe ShouldBe(this AuthorizeInvoiceResponse response)
        {
            return new AuthorizeInvoiceResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static InitializePaymentResponseShouldBe ShouldBe(this InitializePaymentResponse response)
        {
            return new InitializePaymentResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static FinalizeAuthorizationResponseShouldBe ShouldBe(this FinalizeAuthorizationResponse response)
        {
            return new FinalizeAuthorizationResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static RefundPaymentResponseShouldBe ShouldBe(this RefundPaymentResponse response)
        {
            return new RefundPaymentResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static AnnulAuthorizationResponseShouldBe ShouldBe(this AnnulAuthorizationResponse response)
        {
            return new AnnulAuthorizationResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static GetAddressesResponseShouldBe ShouldBe(this GetAddressesResponse response)
        {
            return new GetAddressesResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static GetCustomerProfileResponseShouldBe ShouldBe(this GetCustomerProfileResponse response)
        {
            return new GetCustomerProfileResponseShouldBe(response);
        }

        [DebuggerStepThrough]
        public static PaynovaSdkExceptionShouldBe ShouldBe(this PaynovaSdkException ex)
        {
            return new PaynovaSdkExceptionShouldBe(ex);
        }
    }
}