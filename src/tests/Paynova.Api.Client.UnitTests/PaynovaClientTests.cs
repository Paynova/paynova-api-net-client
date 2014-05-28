using System;
using FluentAssertions;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests
{
    public class PaynovaClientTests : UnitTestsOf<PaynovaClient>
    {
        [MyFact]
        public void When_not_passing_a_connection_It_will_throw_argument_exception()
        {
            Action a = () => SUT = new PaynovaClient(null);

            a.ShouldThrow<ArgumentException>()
                .And.ParamName.Should().Be("connection");
        }
    }
}