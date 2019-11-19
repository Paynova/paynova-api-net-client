using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class LineItemTests : UnitTestsOf<LineItem>
    {
        public LineItemTests()
        {
            const int fakeTaxPercent = 0;
            const int fakeQuantity = 3;
            const decimal fakeUnitAmountExcludingTax = 2.25m;
            const decimal totalLineAmount = (fakeUnitAmountExcludingTax * fakeQuantity) * (1 + fakeTaxPercent);
            const decimal totalLineTaxAmount = (fakeUnitAmountExcludingTax * fakeQuantity) * fakeTaxPercent;

            SUT = new LineItem("some id", "some a.nr", "some name", "litres", fakeTaxPercent, fakeQuantity, fakeUnitAmountExcludingTax, totalLineAmount, totalLineTaxAmount);
        }

        [Fact]
        public void When_passing_tax_percent_less_than_1_and_greater_than_0_It_should_convert_it_to_percent()
        {
            const decimal fakeTaxPercent = 0.25m;

            SUT = new LineItem("some id", "some a.nr", "some name", "litres", fakeTaxPercent, 1, 1, 1, 1);

            SUT.TaxPercent.Should().Be(25m);
        }

        [Fact]
        public void When_passing_tax_percent_1_It_should_get_1_as_percent()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 1m, 2, 2, 2, 2);

            SUT.TaxPercent.Should().Be(1m);
        }

        [Fact]
        public void When_passing_tax_percent_0_It_should_get_0_as_percent()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 0m, 1, 1, 1, 1);

            SUT.TaxPercent.Should().Be(0m);
        }

        [Fact]
        public void When_configuring_line_item_group_key_It_should_update_the_item()
        {
            SUT.WithLineItemGroupKey(i => i.Discount);

            SUT.LineItemGroupKey.Should().Be("_DISCOUNT_");
        }
    }
}