using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class CreateOrderResponseShouldBe: ShouldBe<CreateOrderResponse>
    {
        [DebuggerStepThrough]
        public CreateOrderResponseShouldBe(CreateOrderResponse response) : base(response) { }

        public virtual void Ok()
        {
            Item.ShouldBeSuccessful();
            Item.OrderId.Should().NotBeEmpty();
        }
    }
}