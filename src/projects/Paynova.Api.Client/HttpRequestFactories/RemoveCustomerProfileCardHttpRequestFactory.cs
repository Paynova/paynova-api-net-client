using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public class RemoveCustomerProfileCardHttpRequestFactory : HttpRequestFactoryBase<RemoveCustomerProfileCardRequest>
    {
        public RemoveCustomerProfileCardHttpRequestFactory(IRuntime runtime, ISerializer serializer)
            : base(runtime, serializer)
        {
        }

        protected override string GenerateMethod(RemoveCustomerProfileCardRequest request)
        {
            return HttpMethods.Delete;
        }

        protected override string GenerateRelativeUrl(RemoveCustomerProfileCardRequest request)
        {
            return new RelativeUrlBuilder(Runtime)
                .Register(k => k.ProfileId, request.ProfileId)
                .Register(k => k.CardId, request.CardId)
                .ApplyTo("/customerprofiles/{profileId}/cards/{cardId}");
        }

        protected override string GenerateJson(RemoveCustomerProfileCardRequest request)
        {
            return null;
        }
    }
}