using System;
using System.Collections.Generic;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class UpdateFinalizedLineItemsHttpRequestFactory : HttpRequestFactoryBase<UpdateFinalizedLineItemsRequest>
    {
        public UpdateFinalizedLineItemsHttpRequestFactory(IRuntime runtime, ISerializer serializer) : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(UpdateFinalizedLineItemsRequest request)
        {
            return HttpMethods.Patch;
        }

        protected override string GenerateRelativeUrl(UpdateFinalizedLineItemsRequest request)
        {
            const string relativeUrlFormatWithUpdateOrder = "/orders/{orderId}/transactions/{transactionId}/lineItems";
            const string relativeUrlFormatWithUpdateOrderTransactions = "/transactions/{transactionId}/lineItems";

            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.OrderId, request.OrderId)
                .Register(k => k.TransactionId, request.TransactionId)
                .ApplyTo(request.OrderId != System.Guid.Empty ? relativeUrlFormatWithUpdateOrder : relativeUrlFormatWithUpdateOrderTransactions);
        }
    }
}
