using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    public class UpdateFinalizedLineItemsRequest : Request
    {
        public List<FinalizedLineItemUpdateOperation> Operations { get; private set; }

        /// <summary>
        /// Gets the unique id of the transaction (authorization) that you received from Paynova.
        /// </summary>
        public string TransactionId { get; private set; }

        /// <summary>
        /// Gets the unique identifier that you received from Paynova
        /// in the response from <see cref="IPaynovaClient.CreateOrder(CreateOrderRequest)"/>.
        /// </summary>
        public Guid OrderId { get; private set; }

        /// <summary>
        /// Creates a new instance of the request.
        /// <param name="transactionId">
        /// The unique id of the transaction (authorization) that you received from Paynova.
        /// </param>
        /// </summary>
        public UpdateFinalizedLineItemsRequest(string transactionId)
        {
            Ensure.That(transactionId, "transactionId").IsNotNullOrEmpty();
            TransactionId = transactionId;

            Operations = new List<FinalizedLineItemUpdateOperation>();
        }

        public UpdateFinalizedLineItemsRequest(string transactionId, Guid orderId) : this(transactionId)
        {
            Ensure.That(orderId, "orderId").IsNotEmpty();
            OrderId = orderId;
        }

        public virtual UpdateFinalizedLineItemsRequest UpdateFinalizedLineItemsRequestWithUpdateOperations(FinalizedLineItemUpdateOperation operation)
        {
            Ensure.That(operation, "operation").IsNotNull();
            Operations.Add(operation);
            return this;
        }

        public virtual UpdateFinalizedLineItemsRequest UpdateFinalizedLineItemsRequestWithUpdateOperations(params FinalizedLineItemUpdateOperation[] operations)
        {
            Ensure.That(operations, "operations").HasItems();
            Operations.Clear();
            Operations.AddRange(operations);
            return this;
        }

        public virtual UpdateFinalizedLineItemsRequest UpdateFinalizedLineItemsRequestWithUpdateOperations(IEnumerable<FinalizedLineItemUpdateOperation> operations)
        {
            Ensure.That(operations, "operations").IsNotNull();
            Operations.Clear();
            Operations.AddRange(operations);
            return this;
        }
    }
}