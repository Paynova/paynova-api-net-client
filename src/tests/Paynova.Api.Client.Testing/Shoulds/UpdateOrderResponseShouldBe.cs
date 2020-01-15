using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class UpdateOrderResponseShouldBe : ShouldBe<UpdateFinalizedLineItemsResponse>
    {
        [DebuggerStepThrough]
        public UpdateOrderResponseShouldBe(UpdateFinalizedLineItemsResponse response) : base(response)
        {
        }

        public virtual void AddItem(string transactionId)
        {
            Item.TransactionId.Should().NotBeNullOrEmpty(transactionId);
            Item.Should().NotBeNull(transactionId); Item.ShouldBeSuccessful();
        }

        public virtual void RemovItem(string transactionId)
        {
            Item.TransactionId.Should().NotBeNullOrEmpty(transactionId);
            Item.ShouldBeSuccessful();
        }
    }
}
