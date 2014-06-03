using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class RemoveCustomerProfileHttpRequestFactory : HttpRequestFactoryBase<RemoveCustomerProfileRequest>
    {
        public RemoveCustomerProfileHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(RemoveCustomerProfileRequest request)
        {
            return HttpMethods.Delete;
        }

        protected override string GenerateRelativeUrl(RemoveCustomerProfileRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.ProfileId, request.ProfileId)
                .ApplyTo("/customerprofiles/{profileId}");
        }

        protected override string GenerateJson(RemoveCustomerProfileRequest request)
        {
            return null;
        }
    }
}