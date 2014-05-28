using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class GetCustomerProfileHttpRequestFactoryTests : UnitTestsOf<GetCustomerProfileHttpRequestFactory>
    {
        private const string ProfileId = "myprofile1";

        public GetCustomerProfileHttpRequestFactoryTests()
        {
            SUT = new GetCustomerProfileHttpRequestFactory(Runtime, Serializer);
        }

        [MyFact]
        public void When_creating_request_It_should_render_relative_url_with_profile_id()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().GetAgainst(
                "/customerprofiles/{0}",
                ProfileId);
        }

        [MyFact]
        public void When_creating_request_It_creates_a_http_get_request_without_json()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().GetWithNoJson();
        }

        private GetCustomerProfileRequest CreateRequest()
        {
            return new GetCustomerProfileRequest(ProfileId);
        }
    }
}