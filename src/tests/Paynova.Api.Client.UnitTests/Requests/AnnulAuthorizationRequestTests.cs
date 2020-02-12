using System;
using FluentAssertions;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing.TestData;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Requests
{
    public class AnnulAuthorizationRequestTests : UnitTestsOf<AnnulAuthorizationRequest>
    {
        public AnnulAuthorizationRequestTests()
        {
            SUT = new AnnulAuthorizationRequest("transaction1", new Guid("3cf41244-dbac-48a6-9233-8291842f27bc"), 112.75m);
        }

        [Fact]
        public void When_add_of_line_items_It_should_add_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.LineItems.Length.Should().Be(2);
        }

        [Fact]
        public void When_setting_line_items_It_should_overwrite_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.WithLineItems(LineItemTestData.CreateLineItems(1));

            SUT.LineItems.Length.Should().Be(1);
        }

        [Fact]
        public void When_clearing_line_items_It_should_remove_all_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.ClearLineItems();

            SUT.LineItems.Should().BeEmpty();
        }
    }
}