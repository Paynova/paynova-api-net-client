using System.Linq;
using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Requests
{
    public class CreateOrderRequestTests : UnitTestsOf<CreateOrderRequest>
    {
        public CreateOrderRequestTests()
        {
            SUT = new CreateOrderRequest("order1", CurrencyCode.SwedishKrona, 112.75m);
        }

        [Fact]
        public void When_add_of_line_items_It_should_add_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.AddLineItem(LineItemTestData.CreateLineItems(1).Single());

            SUT.LineItems.Length.Should().Be(3);
        }

        [Fact]
        public void When_setting_line_items_It_should_overwrite_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.WithLineItems(LineItemTestData.CreateLineItems(3));

            SUT.LineItems.Length.Should().Be(3);
        }

        [Fact]
        public void When_clearing_line_items_It_should_remove_all_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.ClearLineItems();

            SUT.LineItems.Should().BeEmpty();
        }

        [Fact]
        public void When_setting_bill_to_using_config_It_should_update_the_request()
        {
            SUT.WithBillTo(c =>
            {
                c.Name.CompanyName = "Some company";
                c.Address.Street1 = "Some street";
            });

            SUT.BillTo.Name.CompanyName.Should().Be("Some company");
            SUT.BillTo.Address.Street1.Should().Be("Some street");
        }

        [Fact]
        public void When_setting_ship_to_using_config_It_should_update_the_request()
        {
            SUT.WithShipTo(c =>
            {
                c.Name.CompanyName = "Some company";
                c.Address.Street1 = "Some street";
            });

            SUT.ShipTo.Name.CompanyName.Should().Be("Some company");
            SUT.ShipTo.Address.Street1.Should().Be("Some street");
        }

        [Fact]
        public void When_setting_customer_using_config_It_should_update_the_request()
        {
            SUT.WithCustomer(c =>
            {
                c.CustomerId = "Some id";
                c.Name.FirstName = "Daniel";
            });

            SUT.Customer.CustomerId.Should().Be("Some id");
            SUT.Customer.Name.FirstName.Should().Be("Daniel");
        }
    }
}