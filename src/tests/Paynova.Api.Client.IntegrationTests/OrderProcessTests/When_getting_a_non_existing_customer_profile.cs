using System;
using FluentAssertions;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.IntegrationTests.OrderProcessTests
{
    public class When_getting_a_non_existing_customer_profile : IntegrationTestsOf<IPaynovaClient>
    {
        public When_getting_a_non_existing_customer_profile()
        {
            SUT = Client;
        }

        [MyFact]
        public void It_should_throw_apaynova_exception()
        {
            Action a = () => SUT.GetCustomerProfile("1fc895e8e99141ecaf52ebd09f6d4a30");
            
            a.ShouldThrow<PaynovaSdkException>();
        }
    }
}