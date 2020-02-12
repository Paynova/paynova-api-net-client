using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class InterfaceIdTests : UnitTestsOf<InterfaceId>
    {
        [Fact]
        public void When_Aero_It_should_be_5()
        {
            InterfaceId.Aero.Should().Be(5);
        }
    }
}