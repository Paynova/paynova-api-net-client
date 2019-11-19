using System.Collections.Specialized;
using FluentAssertions;
using Paynova.Api.Client.Security;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Security
{
    public class PostbackDigestTests : UnitTestsOf<PostbackDigest>
    {
        protected const string CorrectDigestForOnePayment = "6645208808A61B72182CD0A2119B44AC54D1D344";
        public PostbackDigestTests()
        {
            SUT = new PostbackDigest("SECRET_KEYSECRET");
        }

        [Fact]
        public void When_one_payment_attempt_It_can_calculate_the_digest()
        {
            var data = new NameValueCollection
            {
                {"ORDER_ID", "a148910e-226f-43e6-8f72-a33900e2214c"},
                {"SESSION_ID", "775e3b3b-ff40-4078-bdee-a33900e22164"},
                {"ORDER_NUMBER", "7e2e7e8cf17640e5b9da6887eea0860a"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Completed"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281543270574"},
                {"PAYMENT_1_AMOUNT", "100.00"},
            };

            var digest = SUT.Calculate(data);

            digest.Should().Be(CorrectDigestForOnePayment);
        }

        [Fact]
        public void When_three_payment_attempts_It_can_calculate_the_digest()
        {
            var data = new NameValueCollection
            {
                {"ORDER_ID", "e1790349-6965-4e17-bce1-a33900f70eb7"},
                {"SESSION_ID", "f7f83f50-85d0-47d0-962c-a33900f70ecf"},
                {"ORDER_NUMBER", "f86629c047654ea2b286d8b1c6bfc1e9"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Cancelled"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281659372177"},
                {"PAYMENT_1_AMOUNT", "100.00"},
                {"PAYMENT_2_STATUS", "Failed"},
                {"PAYMENT_2_TRANSACTION_ID", "201405281659432478"},
                {"PAYMENT_2_AMOUNT", "100.00"},
                {"PAYMENT_3_STATUS", "Authorized"},
                {"PAYMENT_3_TRANSACTION_ID", "201405281659579809"},
                {"PAYMENT_3_AMOUNT", "100.00"},
            };

            var digest = SUT.Calculate(data);

            digest.Should().Be("94E37F5EDF86B734DA198D5EC01350FF0949E636");
        }

        [Fact]
        public void When_one_payment_attempt_It_should_validate_to_true()
        {
            var data = new NameValueCollection
            {
                {"ORDER_ID", "a148910e-226f-43e6-8f72-a33900e2214c"},
                {"SESSION_ID", "775e3b3b-ff40-4078-bdee-a33900e22164"},
                {"ORDER_NUMBER", "7e2e7e8cf17640e5b9da6887eea0860a"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Completed"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281543270574"},
                {"PAYMENT_1_AMOUNT", "100.00"},
                {"DIGEST", CorrectDigestForOnePayment}
            };

            var isValid = SUT.Validate(data);

            isValid.Should().BeTrue();
        }

        [Fact]
        public void When_one_payment_attempt_It_should_validate_to_true_and_return_the_digest()
        {
            var data = new NameValueCollection
            {
                {"ORDER_ID", "a148910e-226f-43e6-8f72-a33900e2214c"},
                {"SESSION_ID", "775e3b3b-ff40-4078-bdee-a33900e22164"},
                {"ORDER_NUMBER", "7e2e7e8cf17640e5b9da6887eea0860a"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Completed"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281543270574"},
                {"PAYMENT_1_AMOUNT", "100.00"},
                {"DIGEST", CorrectDigestForOnePayment}
            };

            string calculatedDigest;
            var isValid = SUT.TryValidate(data, out calculatedDigest);

            isValid.Should().BeTrue();
            calculatedDigest.Should().Be(CorrectDigestForOnePayment);
        }

        [Fact]
        public void When_wrong_data_It_should_validate_to_false()
        {
            var data = new NameValueCollection
            {
                {"ORDER_ID", "a148910e-226f-43e6-8f72-a33900e2214c"},
                {"SESSION_ID", "775e3b3b-ff40-4078-bdee-a33900e22164"},
                {"ORDER_NUMBER", "7e2e7e8cf17640e5b9da6887eea0860a"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Completed"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281543270574"},
                {"PAYMENT_1_AMOUNT", "100.00"},
                {"DIGEST", "Some fake digest sent from Paynova"}
            };

            var isValid = SUT.Validate(data);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void When_wrong_data_It_should_validate_to_false_and_return_the_digest()
        {
            const string fakeDigest = "Some fake digest sent from Paynova";
            var data = new NameValueCollection
            {
                {"ORDER_ID", "a148910e-226f-43e6-8f72-a33900e2214c"},
                {"SESSION_ID", "775e3b3b-ff40-4078-bdee-a33900e22164"},
                {"ORDER_NUMBER", "7e2e7e8cf17640e5b9da6887eea0860a"},
                {"SESSION_STATUS", "Completed"},
                {"CURRENCY_CODE", "SEK"},
                {"PAYMENT_1_STATUS", "Completed"},
                {"PAYMENT_1_TRANSACTION_ID", "201405281543270574"},
                {"PAYMENT_1_AMOUNT", "100.00"},
                {"DIGEST", fakeDigest}
            };

            string calculatedDigest;
            var isValid = SUT.TryValidate(data, out calculatedDigest);

            isValid.Should().BeFalse();
            calculatedDigest.Should().NotBe(fakeDigest);
            calculatedDigest.Should().Be(CorrectDigestForOnePayment);
        }
    }
}