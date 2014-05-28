using System;
using System.Net;
using FluentAssertions;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.TestData;

namespace Paynova.Api.Client.IntegrationTests.ClientTests
{
    public class When_wrong_server_address : IntegrationTestsOf<IPaynovaClient>
    {
        public When_wrong_server_address()
        {
            SUT = new PaynovaClient("http://fdac85aaeadd41ce91e7eacb402034b3.paynova.com", "fake", "fake");
        }

        [MyFact]
        public void It_should_throw_a_web_exception_indicating_name_resolution_failure()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateSimple("order1");

            Action a = () => SUT.CreateOrder(createOrderRequest);

            a.ShouldThrow<WebException>()
                .And.Status.Should().Be(WebExceptionStatus.NameResolutionFailure);
        }
    }
}