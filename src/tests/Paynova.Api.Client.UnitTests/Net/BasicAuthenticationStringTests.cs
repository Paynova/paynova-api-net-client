using FluentAssertions;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Net
{
    public class BasicAuthenticationStringTests : UnitTestsOf<BasicAuthenticationStringTests>
    {
        [MyFact]
        public void When_passing_username_and_password_It_initializes_a_base64_encoded_string()
        {
            var sut = new BasicAuthenticationString("testUser", "testPassword");

            sut.Value.Should().Be("dGVzdFVzZXI6dGVzdFBhc3N3b3Jk");
            sut.ToString().Should().Be(sut.Value);
            ((string) sut).Should().Be(sut.Value);
        }
    }
}