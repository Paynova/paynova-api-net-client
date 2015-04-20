using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class GetPaymentOptionsResponseShouldBe : ShouldBe<GetPaymentOptionsResponse>
    {
        [DebuggerStepThrough]
        public GetPaymentOptionsResponseShouldBe(GetPaymentOptionsResponse response) : base(response) {}

        public virtual void Ok()
        {
            Item.ShouldBeSuccessful();
            Item.AvailablePaymentMethods.Should().NotBeNull();
            Item.AvailablePaymentMethods.Should().NotBeEmpty();
            foreach (var pm in Item.AvailablePaymentMethods)
            {
                pm.PaymentMethodId.Should().BeGreaterThan(0);
                pm.DisplayName.Should().NotBeNullOrWhiteSpace();
                pm.Group.Should().NotBeNull();
                pm.Group.Key.Should().NotBeNullOrWhiteSpace();
                pm.Group.DisplayName.Should().NotBeNullOrWhiteSpace();
            }
        }

        public virtual void Containing(params PaymentMethodDetail[] paymentMethodDetails)
        {
            Item.ShouldBe().Ok();
            Item.AvailablePaymentMethods.ShouldBeValueEqualTo(paymentMethodDetails); //ShouldBeEquivalentTo didn't work
        }
    }
}