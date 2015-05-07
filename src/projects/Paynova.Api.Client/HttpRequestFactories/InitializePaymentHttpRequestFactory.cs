using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class InitializePaymentHttpRequestFactory : HttpRequestFactoryBase<InitializePaymentRequest>
    {
        public InitializePaymentHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(InitializePaymentRequest request)
        {
            return HttpMethods.Post;
        }

        protected override string GenerateRelativeUrl(InitializePaymentRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.OrderId, request.OrderId)
                .ApplyTo("/orders/{orderId}/initializePayment");
        }
    }
}