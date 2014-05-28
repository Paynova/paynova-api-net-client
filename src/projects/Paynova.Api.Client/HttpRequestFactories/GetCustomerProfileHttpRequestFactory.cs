using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class GetCustomerProfileHttpRequestFactory : HttpRequestFactoryBase<GetCustomerProfileRequest>
    {
        public GetCustomerProfileHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(GetCustomerProfileRequest request)
        {
            return HttpMethods.Get;
        }

        protected override string GenerateRelativeUrl(GetCustomerProfileRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.ProfileId, request.ProfileId)
                .ApplyTo("/customerprofiles/{profileId}");
        }

        protected override string GenerateJson(GetCustomerProfileRequest request)
        {
            return null;
        }
    }
}