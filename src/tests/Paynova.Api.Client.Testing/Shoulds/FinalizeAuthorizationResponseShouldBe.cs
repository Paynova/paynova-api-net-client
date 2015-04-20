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
            Item.Should().NotBeNull();
            Item.TransactionId.Should().NotBeNullOrWhiteSpace();
            Item.AcquirerId.Should().NotBeNullOrWhiteSpace();
            Item.TotalAmountFinalized.Should().Be(totalAmount);
            Item.AmountRemainingForFinalization.Should().Be(0);
            Item.TotalAmountPendingFinalization.Should().Be(0);
            Item.CanFinalizeAgain.Should().BeFalse();
            Item.Status.Should().NotBeNull();
            Item.Status.StatusKey.Should().Be("SUCCESS");
            Item.Status.StatusMessage.Should().Be("The operation was successful.");
        }
    }
}