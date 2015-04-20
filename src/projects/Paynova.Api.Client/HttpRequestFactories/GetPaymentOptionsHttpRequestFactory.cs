using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class GetPaymentOptionsHttpRequestFactory : HttpRequestFactoryBase<GetPaymentOptionsRequest>
    {
        public GetPaymentOptionsHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(GetPaymentOptionsRequest request)
        {
            return HttpMethods.Post;
        }

        protected override string GenerateRelativeUrl(GetPaymentOptionsRequest request)
        {
            return "/paymentoptions";
        }
    }
}