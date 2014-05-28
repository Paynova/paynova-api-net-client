using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class CreateOrderHttpRequestFactory : HttpRequestFactoryBase<CreateOrderRequest>
    {
        public CreateOrderHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(CreateOrderRequest request)
        {
            return HttpMethods.Post;
        }

        protected override string GenerateRelativeUrl(CreateOrderRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.OrderNumber, request.OrderNumber)
                .Register(k => k.TotalAmount, request.TotalAmount)
                .Register(k => k.CurrencyCode, request.CurrencyCode)
                .ApplyTo("orders/create/{orderNumber}/{totalAmount}/{currencyCode}");
        }
    }
}