using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class LineItemGroupKeysTests : UnitTestsOf<LineItemGroupKeys>
    {
        public LineItemGroupKeysTests()
        {
            SUT = LineItemGroupKeys.Instance;
        }

        [Fact]
        public void It_has_correct_value_for_Discount()
        {
            SUT.Discount.Should().Be("_DISCOUNT_");
        }

        [Fact]
        public void It_has_correct_value_for_Extra()
        {
            SUT.Extra.Should().Be("_EXTRA_");
        }

        [Fact]
        public void It_has_correct_value_for_Shipping()
        {
            SUT.Shipping.Should().Be("_SHIPPING_");
        }

        [Fact]
        public void It_has_correct_value_for_Tax()
        {
            SUT.Tax.Should().Be("_TAX_");
        }
    }
}