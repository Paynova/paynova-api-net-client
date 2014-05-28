using System.Collections.Specialized;
using FluentAssertions;
using Paynova.Api.Client.Security;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Security
{
    public class CallbackDigestTests : UnitTestsOf<CallbackDigest>
    {
        public CallbackDigestTests()
        {
            SUT = new CallbackDigest();
        }

        [MyFact]
        public void Can_calculate_the_digest()
        {
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
            };

            var digest = SUT.Calculate(data, "SECRET_KEYSECRET");

            digest.Should().Be("89283237A406B7D5BE3FAC3F5AC512B093DEFD78");
        }
    }
}