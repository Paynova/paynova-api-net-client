using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class GetPaymentOptionsHttpRequestFactoryTests : UnitTestsOf<GetPaymentOptionsHttpRequestFactory>
    {
        public GetPaymentOptionsHttpRequestFactoryTests()
        {
            SUT = new GetPaymentOptionsHttpRequestFactory(Runtime, Serializer);
        }

        [MyFact]
        public void When_creating_request_It_should_render_correct_relative_url()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostAgainst("/paymentoptions");
        }

        [MyFact]
        public void When_creating_request_It_creates_a_http_post_request_with_correct_json()
        {
            var request = CreateRequest();

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.GetPaymentOptions);
        }

        private GetPaymentOptionsRequest CreateRequest()
        {
            return new GetPaymentOptionsRequest(112.75m, PaymentChannelId.Web, CurrencyCode.SwedishKrona, "SE", "SWE");
        }
    }
}