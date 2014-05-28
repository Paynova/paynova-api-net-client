using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class LineItemGroupKeysTests : UnitTestsOf<LineItemGroupKeys>
    {
        public LineItemGroupKeysTests()
        {
            SUT = LineItemGroupKeys.Instance;
        }

        [MyFact]
        public void It_has_correct_value_for_Discount()
        {
            SUT.Discount.Should().Be("_DISCOUNT_");
        }

        [MyFact]
        public void It_has_correct_value_for_Extra()
        {
            SUT.Extra.Should().Be("_EXTRA_");
        }

        [MyFact]
        public void It_has_correct_value_for_Shipping()
        {
            SUT.Shipping.Should().Be("_SHIPPING_");
        }

        [MyFact]
        public void It_has_correct_value_for_Tax()
        {
            SUT.Tax.Should().Be("_TAX_");
        }
    }
}