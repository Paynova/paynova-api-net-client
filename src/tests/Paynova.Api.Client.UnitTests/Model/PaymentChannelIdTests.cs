using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class PaymentChannelIdTests : UnitTestsOf<PaymentChannelId>
    {
        [Fact]
        public void When_Web_It_should_be_1()
        {
            PaymentChannelId.Web.Should().Be(1);
        }

        [Fact]
        public void When_Mail_It_should_be_2()
        {
            PaymentChannelId.Mail.Should().Be(2);
        }

        [Fact]
        public void When_Telephone_It_should_be_2()
        {
            PaymentChannelId.Telephone.Should().Be(2);
        }

        [Fact]
        public void When_Recurring_It_should_be_7()
        {
            PaymentChannelId.Recurring.Should().Be(7);
        }
    }
}