using System;
using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.Shoulds;
using Xunit;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class RemoveCustomerProfileCardHttpRequestFactoryTests : UnitTestsOf<RemoveCustomerProfileCardHttpRequestFactory>
    {
        private const string ProfileId = "myprofile1";
        private readonly Guid _cardId = new Guid("d2abdbfe-ed4a-4907-8d0c-ff620c21e895");

        public RemoveCustomerProfileCardHttpRequestFactoryTests()
        {
            SUT = new RemoveCustomerProfileCardHttpRequestFactory(Runtime, Serializer);
        }

        [Fact]
        public void When_creating_request_It_should_render_relative_url_with_profile_and_card_id()
        {
            var request = new RemoveCustomerProfileCardRequest(ProfileId, _cardId);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().DeleteAgainst(
                "/customerprofiles/{0}/cards/{1}",
                ProfileId,
                _cardId.ToString("n"));
        }

        [Fact]
        public void When_creating_request_It_creates_a_http_delete_request_without_json()
        {
            var request = new RemoveCustomerProfileCardRequest(ProfileId, _cardId);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().DeleteWithNoJson();
        }
    }
}