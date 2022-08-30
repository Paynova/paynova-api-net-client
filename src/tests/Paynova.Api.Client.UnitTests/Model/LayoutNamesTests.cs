using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class LayoutNamesTests : UnitTestsOf<LayoutNames>
    {
        public LayoutNamesTests()
        {
            SUT = LayoutNames.Instance;
        }

        [Fact]
        public void It_has_correct_value_for_Desktop()
        {
            SUT.Desktop.Should().Be("Paynova_FullPage_1");
        }

        [Fact]
        public void It_has_correct_value_for_Mobile()
        {
            SUT.Mobile.Should().Be("Paynova_Mobile_1");
        }
    }
}