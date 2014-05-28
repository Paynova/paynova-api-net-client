using FluentAssertions;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Net
{
    public class HttpRequestTests : UnitTestsOf<HttpRequest>
    {
        [MyFact]
        public void When_setting_json_It_should_set_content_and_type()
        {
            const string json = "{\"msg\":\"test\"}";
            SUT = new HttpRequest("orders/1", "GET");

            SUT.SetJson(json);

            SUT.Content.Should().Be(json);
            SUT.ContentType.Should().Be("application/json");
        }

        [MyFact]
        public void When_request_has_content_It_has_content_should_be_true()
        {
            SUT = new HttpRequest("orders/1", "GET");

            SUT.SetJson("{\"msg\":\"test\"}");

            SUT.HasContent().Should().BeTrue();
        }

        [MyFact]
        public void When_request_has_no_content_It_has_content_should_be_false()
        {
            SUT = new HttpRequest("orders/1", "GET");

            SUT.HasContent().Should().BeFalse();
        }
    }
}