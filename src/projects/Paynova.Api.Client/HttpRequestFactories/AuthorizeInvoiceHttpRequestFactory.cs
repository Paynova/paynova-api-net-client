using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class AuthorizeInvoiceHttpRequestFactory : HttpRequestFactoryBase<AuthorizeInvoiceRequest>
    {
        public AuthorizeInvoiceHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(AuthorizeInvoiceRequest request)
        {
            return HttpMethods.Post;
        }

        protected override string GenerateRelativeUrl(AuthorizeInvoiceRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.OrderId, request.OrderId)
                .ApplyTo("/orders/{orderId}/authorizePayment");
        }
    }
}