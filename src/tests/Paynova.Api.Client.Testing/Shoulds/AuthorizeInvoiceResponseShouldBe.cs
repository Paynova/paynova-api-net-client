using System;
using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class AuthorizeInvoiceResponseShouldBe : ShouldBe<AuthorizeInvoiceResponse>
    {
        [DebuggerStepThrough]
        public AuthorizeInvoiceResponseShouldBe(AuthorizeInvoiceResponse response) : base(response) { }

        public virtual void Ok(Guid? orderId = null)
        {
            Item.Should().NotBeNull();
            Item.OrderId.Should().NotBeEmpty();
            if (orderId.HasValue)
                Item.OrderId.Should().Be(orderId.Value);

            Item.TransactionId.Should().NotBeNullOrWhiteSpace();
            Item.AcquirerId.Should().BeGreaterThan(0);
            Item.AcquirerReferenceId.Should().NotBeNullOrWhiteSpace();
            Item.Status.Should().NotBeNull();
            Item.Status.StatusKey.Should().Be("SUCCESS");
            Item.Status.StatusMessage.Should().Be("The operation was successful.");
        }
    }
}