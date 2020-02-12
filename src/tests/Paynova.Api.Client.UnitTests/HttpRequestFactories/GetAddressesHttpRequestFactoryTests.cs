using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.Shoulds;
using Xunit;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class GetAddressesHttpRequestFactoryTests : UnitTestsOf<GetAddressesHttpRequestFactory>
    {
        private const string CountryCode = "SE";
        private const string GovernmentId = "198005039212";

        public GetAddressesHttpRequestFactoryTests()
        {
            SUT = new GetAddressesHttpRequestFactory(Runtime, Serializer);
        }

        [Fact]
        public void When_creating_request_It_should_render_relative_url_with_profile_id()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().GetAgainst(
                "/addresses/{0}/{1}",
                CountryCode,
                GovernmentId);
        }

        [Fact]
        public void When_creating_request_It_creates_a_http_get_request_without_json()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().GetWithNoJson();
        }

        private GetAddressesRequest CreateRequest()
        {
            return new GetAddressesRequest(CountryCode, GovernmentId);
        }
    }
}