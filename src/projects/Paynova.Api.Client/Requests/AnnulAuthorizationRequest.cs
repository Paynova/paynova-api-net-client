using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    public class AnnulAuthorizationRequest : Request
    {
        protected List<LineItem> InternalLineItems { get; private set; }

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
        /// Gets the total amount to annul. The amount must be equal to or less than the original authorized amount.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets the line items included in this Annulization.
        /// </summary>
        public LineItem[] LineItems { get { return InternalLineItems.ToArray(); } }

        /// <summary>
        /// Creates a new instance of the request.
        /// </summary>
        /// <param name="transactionId">
        /// The unique id of the transaction (authorization) that you received from Paynova.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount to annul. The amount must be equal to or less than the original authorized amount.
        /// </param>
        public AnnulAuthorizationRequest(string transactionId, decimal totalAmount)
        {
            Ensure.That(transactionId, "transactionId").IsNotNullOrEmpty();
            Ensure.That(totalAmount, "totalAmount").IsGt(0);

            TransactionId = transactionId;
            TotalAmount = totalAmount;

            InternalLineItems = new List<LineItem>();
        }

        /// <summary>
        /// Creates a new instance of the request.
        /// </summary>
        /// <param name="transactionId">
        /// The unique id of the transaction (authorization) that you received from Paynova.
        /// </param>
        /// <param name="orderId">
        /// The unique id you received from Paynova to identify the order.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount to annul. The amount must be equal to or less than the original authorized amount.
        /// </param>
        public AnnulAuthorizationRequest(string transactionId, Guid orderId, decimal totalAmount)
            : this(transactionId, totalAmount)
        {
            Ensure.That(orderId, "orderId").IsNotEmpty();

            OrderId = orderId;
        }

        public virtual AnnulAuthorizationRequest AddLineItem(LineItem item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalLineItems.Add(item);

            return this;
        }

        public virtual AnnulAuthorizationRequest ClearLineItems()
        {
            InternalLineItems.Clear();

            return this;
        }

        public virtual AnnulAuthorizationRequest WithLineItems(params LineItem[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }

        public virtual AnnulAuthorizationRequest WithLineItems(IEnumerable<LineItem> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }
    }
}