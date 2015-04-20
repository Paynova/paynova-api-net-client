using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class FinalizeAuthorizationResponseShouldBe : ShouldBe<FinalizeAuthorizationResponse>
    {
        [DebuggerStepThrough]
        public FinalizeAuthorizationResponseShouldBe(FinalizeAuthorizationResponse response) : base(response) { }

        public virtual void AuthorizedInFull(decimal totalAmount)
        {
            Item.ShouldBeSuccessful();
            Item.TransactionId.Should().NotBeNullOrWhiteSpace();
            Item.AcquirerId.Should().NotBeNullOrWhiteSpace();
            Item.TotalAmountFinalized.Should().Be(totalAmount);
            Item.AmountRemainingForFinalization.Should().Be(0);
            Item.TotalAmountPendingFinalization.Should().Be(0);
            Item.CanFinalizeAgain.Should().BeFalse();
        }
    }
}