using System;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class AnnulAuthorizationHttpRequestFactory : HttpRequestFactoryBase<AnnulAuthorizationRequest>
    {
        public AnnulAuthorizationHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(AnnulAuthorizationRequest request)
        {
            return HttpMethods.Post;
        }

        protected override string GenerateRelativeUrl(AnnulAuthorizationRequest request)
        {
            const string relativeUrlFormatWithOrder = "/orders/{orderId}/transactions/{transactionId}/annul/{totalAmount}";
            const string relativeUrlFormatWithoutOrder = "/transactions/{transactionId}/annul/{totalAmount}";

            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.OrderId, request.OrderId)
                .Register(k => k.TransactionId, request.TransactionId)
                .Register(k => k.TotalAmount, request.TotalAmount)
                .ApplyTo(request.OrderId != Guid.Empty ? relativeUrlFormatWithOrder : relativeUrlFormatWithoutOrder);
        }
    }
}