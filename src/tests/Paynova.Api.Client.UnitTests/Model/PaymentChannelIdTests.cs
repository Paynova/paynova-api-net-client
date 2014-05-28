using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class PaymentChannelIdTests : UnitTestsOf<PaymentChannelId>
    {
        [MyFact]
        public void When_Web_It_should_be_1()
        {
            PaymentChannelId.Web.Should().Be(1);
        }

        [MyFact]
        public void When_Mail_It_should_be_2()
        {
            PaymentChannelId.Mail.Should().Be(2);
        }

        [MyFact]
        public void When_Telephone_It_should_be_2()
        {
            PaymentChannelId.Telephone.Should().Be(2);
        }

        [MyFact]
        public void When_Recurring_It_should_be_7()
        {
            PaymentChannelId.Recurring.Should().Be(7);
        }
    }
}