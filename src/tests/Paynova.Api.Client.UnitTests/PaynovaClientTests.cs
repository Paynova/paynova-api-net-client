using System;
using FluentAssertions;
using Xunit;

namespace Paynova.Api.Client.UnitTests
{
    public class PaynovaClientTests : UnitTestsOf<PaynovaClient>
    {
        [Fact]
        public void When_not_passing_a_connection_It_will_throw_argument_exception()
        {
            Action a = () => SUT = new PaynovaClient(null);

            a.Should().Throw<ArgumentException>()
                .And.ParamName.Should().Be("connection");
        }
    }
}