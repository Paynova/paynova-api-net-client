using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class LineItemTests : UnitTestsOf<LineItem>
    {
        public LineItemTests()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 0, 3, 2.25m);
        }

        [MyFact]
        public void When_passing_tax_percent_less_than_1_and_greater_than_0_It_should_convert_it_to_percent()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 0.25m, 1, 1);

            SUT.TaxPercent.Should().Be(25m);
        }

        [MyFact]
        public void When_passing_tax_percent_1_It_should_get_1_as_percent()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 1m, 1, 1);

            SUT.TaxPercent.Should().Be(1m);
        }

        [MyFact]
        public void When_passing_tax_percent_0_It_should_get_0_as_percent()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 0m, 1, 1);

            SUT.TaxPercent.Should().Be(0m);
        }

        [MyFact]
        public void When_tax_percent_is_positive_factor_It_should_calcuate_prices_correctly()
        {
            SUT = new LineItem("some id", "some a.nr", "some name", "litres", 0.25m, 3, 2.25m);

            SUT.Quantity.Should().Be(3);
            SUT.UnitAmountExcludingTax.Should().Be(2.25m);
            SUT.TotalLineAmount.Should().Be(8.4375m);
            SUT.TotalLineTaxAmount.Should().Be(1.6875m);
        }

        [MyFact]
        public void When_tax_percent_is_zero_It_should_calcuate_prices_correctly()
        {
            SUT.Quantity.Should().Be(3);
            SUT.UnitAmountExcludingTax.Should().Be(2.25m);
            SUT.TotalLineAmount.Should().Be(6.75m);
            SUT.TotalLineTaxAmount.Should().Be(0m);
        }

        [MyFact]
        public void When_configuring_line_item_group_key_It_should_update_the_item()
        {
            SUT.WithLineItemGroupKey(i => i.Discount);

            SUT.LineItemGroupKey.Should().Be("_DISCOUNT_");
        }
    }
}