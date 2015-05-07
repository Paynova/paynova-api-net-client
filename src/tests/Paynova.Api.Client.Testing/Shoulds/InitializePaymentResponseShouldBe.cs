using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class InitializePaymentResponseShouldBe : ShouldBe<InitializePaymentResponse>
    {
        [DebuggerStepThrough]
        public InitializePaymentResponseShouldBe(InitializePaymentResponse response) : base(response) { }

        public virtual void Ok()
        {
            Item.ShouldBeSuccessful();
            Item.SessionId.Should().NotBeEmpty();
            Item.Url.Should().NotBeNullOrWhiteSpace();
        }
    }
}