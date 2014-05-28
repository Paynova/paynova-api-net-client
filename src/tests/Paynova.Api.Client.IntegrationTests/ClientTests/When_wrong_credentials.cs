using System;
using System.Net;
using FluentAssertions;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.TestData;

namespace Paynova.Api.Client.IntegrationTests.ClientTests
{
    public class When_wrong_credentials : IntegrationTestsOf<IPaynovaClient>
    {
        public When_wrong_credentials()
        {
            SUT = new PaynovaClient(Client.Connection.ServerAddress, "fake", "fake");
        }

        [MyFact]
        public void It_should_throw_a_web_exception_indicating_unauthorized()
        {
            var createOrderRequest = CreateOrderRequestTestData.CreateSimple("order1");

            Action a = () => SUT.CreateOrder(createOrderRequest);
            a.ShouldThrow<WebException>()
                .And.Response.As<HttpWebResponse>()
                .StatusCode.Should()
                .Be(HttpStatusCode.Unauthorized);
        }
    }
}