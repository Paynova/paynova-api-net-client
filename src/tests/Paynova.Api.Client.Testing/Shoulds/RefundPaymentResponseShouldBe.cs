using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class RefundPaymentResponseShouldBe : ShouldBe<RefundPaymentResponse>
    {
        [DebuggerStepThrough]
        public RefundPaymentResponseShouldBe(RefundPaymentResponse response) : base(response) { }

        public virtual void RefundedInFull(decimal totalAmount)
        {
            Item.Should().NotBeNull();
            Item.TransactionId.Should().NotBeNullOrWhiteSpace();
            Item.AcquirerId.Should().NotBeNullOrWhiteSpace();
            Item.TotalAmountRefunded.Should().Be(totalAmount);
            Item.AmountRemainingForRefund.Should().Be(0);
            Item.TotalAmountPendingRefund.Should().Be(0);
            Item.CanRefundAgain.Should().BeFalse();
            Item.Status.Should().NotBeNull();
            Item.Status.StatusKey.Should().Be("SUCCESS");
            Item.Status.StatusMessage.Should().Be("The operation was successful.");
        }

        public virtual void PartiallyRefunded(decimal totalAmount)
        {
            Item.Should().NotBeNull();
            Item.TransactionId.Should().NotBeNullOrWhiteSpace();
            Item.AcquirerId.Should().NotBeNullOrWhiteSpace();
            Item.TotalAmountRefunded.Should().Be(totalAmount);
            Item.AmountRemainingForRefund.Should().BeGreaterThan(0);
            Item.TotalAmountPendingRefund.Should().Be(0);
            Item.CanRefundAgain.Should().BeTrue();
            Item.Status.Should().NotBeNull();
            Item.Status.StatusKey.Should().Be("SUCCESS");
            Item.Status.StatusMessage.Should().Be("The operation was successful.");
        }
    }
}