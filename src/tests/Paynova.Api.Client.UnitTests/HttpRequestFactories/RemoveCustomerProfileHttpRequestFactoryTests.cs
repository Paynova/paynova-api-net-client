using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class RemoveCustomerProfileHttpRequestFactoryTests : UnitTestsOf<RemoveCustomerProfileHttpRequestFactory>
    {
        private const string ProfileId = "myprofile1";

        public RemoveCustomerProfileHttpRequestFactoryTests()
        {
            SUT = new RemoveCustomerProfileHttpRequestFactory(Runtime, Serializer);
        }

        [MyFact]
        public void When_creating_request_It_should_render_relative_url_with_profile_and_card_id()
        {
            var request = new RemoveCustomerProfileRequest(ProfileId);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().DeleteAgainst(
                "/customerprofiles/{0}",
                ProfileId);
        }

        [MyFact]
        public void When_creating_request_It_creates_a_http_delete_request_without_json()
        {
            var request = new RemoveCustomerProfileRequest(ProfileId);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().DeleteWithNoJson();
        }
    }
}