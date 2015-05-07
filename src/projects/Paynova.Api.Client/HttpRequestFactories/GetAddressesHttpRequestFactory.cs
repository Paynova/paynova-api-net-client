using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class GetAddressesHttpRequestFactory : HttpRequestFactoryBase<GetAddressesRequest>
    {
        public GetAddressesHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(GetAddressesRequest request)
        {
            return HttpMethods.Get;
        }

        protected override string GenerateRelativeUrl(GetAddressesRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.CountryCode, request.CountryCode)
                .Register(k => k.GovernmentId, request.GovernmentId)
                .ApplyTo("/addresses/{countryCode}/{governmentId}");
        }

        protected override string GenerateJson(GetAddressesRequest request)
        {
            return null;
        }
    }
}