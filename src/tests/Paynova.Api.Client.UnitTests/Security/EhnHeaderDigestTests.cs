using FluentAssertions;
using Paynova.Api.Client.Security;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Security
{
    public class EhnHeaderDigestTests : UnitTestsOf<EhnHeaderDigest>
    {
        public EhnHeaderDigestTests()
        {
            SUT = new EhnHeaderDigest("SECRET_KEYSECRET");
        }

        [MyFact]
        public void Can_calculate_the_digest()
        {
            var data = "CUSTOM_DATA_COUNT=0&DELIVERY_TIMESTAMP=2014-05-30+08%3a07%3a54Z&DIGEST=1E16B78487A4086E36B3BD07B87BA7CF7DB61981"
                       + "&EVENT_TIMESTAMP=2014-05-30+08%3a07%3a54Z&EVENT_TYPE=SESSION_END&MERCHANT_ID=1262823"
                       + "&ORDER_ID=8cf4f5d8-a09c-458b-a0ef-a33b0085f589&ORDER_NUMBER=e392501f92dc423397f1864c24457ddb"
                       + "&SESSION_ID=9f963a0c-4838-4760-bb12-a33b0085f5a1&SESSION_STATUS=COMPLETED&SESSION_STATUS_REASON=PAYMENT_COMPLETED";

            var digest = SUT.Calculate(data);

            digest.Should().Be("72A72DD41B50C265847670A1566E152E2E4DA466");
        }
    }
}