using FluentAssertions;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class InterfaceOptionsTests : UnitTestsOf<InterfaceOptions>
    {
        public InterfaceOptionsTests()
        {
            SUT = new InterfaceOptions(
                InterfaceId.Aero,
                "ENG",
                "http://api.test.com/paynova/sucess".ToUri(),
                "http://api.test.com/paynova/cancel".ToUri(),
                "http://api.test.com/paynova/pending".ToUri());
        }

        [MyFact]
        public void When_configuring_line_item_group_key_It_should_update_the_item()
        {
            SUT.WithLayoutName(i => i.Desktop);

            SUT.LayoutName.Should().Be("Paynova_FullPage_1");
        }
    }
}