using System.Collections.Specialized;
using FluentAssertions;
using Paynova.Api.Client.Security;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Security
{
    public class EhnDigestTests : UnitTestsOf<EhnDigest>
    {
        protected const string CorrectDigest = "89283237A406B7D5BE3FAC3F5AC512B093DEFD78";

        public EhnDigestTests()
        {
            SUT = new EhnDigest("SECRET_KEYSECRET");
        }

        [MyFact]
        public void When_correct_It_can_calculate_the_digest()
        {
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
            };

            var digest = SUT.Calculate(data);

            digest.Should().Be(CorrectDigest);
        }

        [MyFact]
        public void When_correct_It_should_validate_to_true()
        {
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
                {"DIGEST", CorrectDigest}
            };

            var isValid = SUT.Validate(data);

            isValid.Should().BeTrue();
        }

        [MyFact]
        public void When_correct_It_should_validate_to_true_and_return_the_digest()
        {
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
                {"DIGEST", CorrectDigest}
            };

            string calculatedDigest;
            var isValid = SUT.TryValidate(data, out calculatedDigest);

            isValid.Should().BeTrue();
            calculatedDigest.Should().Be(CorrectDigest);
        }

        [MyFact]
        public void When_wrong_data_It_should_validate_to_false()
        {
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
                {"DIGEST", "Some fake digest sent from Paynova"}
            };

            var isValid = SUT.Validate(data);

            isValid.Should().BeFalse();
        }

        [MyFact]
        public void When_wrong_data_It_should_validate_to_false_and_return_the_digest()
        {
            const string fakeDigest = "Some fake digest sent from Paynova";
            var data = new NameValueCollection
            {
                {"EVENT_TYPE", "SESSION_END"},
                {"EVENT_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"DELIVERY_TIMESTAMP", "2014-05-28 15:00:29Z"},
                {"MERCHANT_ID", "1262823"},
                {"DIGEST", fakeDigest}
            };

            string calculatedDigest;
            var isValid = SUT.TryValidate(data, out calculatedDigest);

            isValid.Should().BeFalse();
            calculatedDigest.Should().NotBe(fakeDigest);
            calculatedDigest.Should().Be(CorrectDigest);
        }
    }
}