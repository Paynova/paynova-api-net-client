using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    /// <summary>
    /// Represents the request used to create an order within Paynova's system.
    /// <![CDATA[http://docs.paynova.com]]>
    /// </summary>
    public class CreateOrderRequest : Request
    {
        protected List<LineItem> InternalLineItems { get; private set; }

        /// <summary>
        /// Gets the provided identifier for the order.
        /// Most likely from your order management system.
        /// </summary>
        public string OrderNumber { get; private set; }

        /// <summary>
        /// Gets or sets an arbitrary and optional description of the order.
        /// This information is stored on the transaction level.
        /// </summary>
        public string OrderDescription { get; set; }

        /// <summary>
        /// Gets the three-letter (alpha-3) ISO currency code or currency number as per ISO 4217.
        /// Example: SEK, 752
        /// </summary>
        public string CurrencyCode { get; private set; }

        /// <summary>
        /// Gets the total amount of the order. If line items are provided,
        /// the value of this field must equal the total sum of all line items.
        /// </summary>
        /// <remarks>For Japanese Yuan (JPY), only whole numbers are allowed.</remarks>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets or sets your reference for the sales channel through which
        /// the customer is purchasing goods/services.
        /// </summary>
        public string SalesChannel { get; set; }

        /// <summary>
        /// Gets or sets your identifier for the sales location.
        /// This might be a website URL, a country, a call-center location, etc.
        /// </summary>
        public string SalesLocationId { get; set; }

        /// <summary>
        /// Gets or sets information about the customer.
        /// </summary>
        /// <remarks>
        /// Information contained within this parameter is required for
        /// certain payment methods, and if fraud screening is used.
        /// </remarks>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the billing name and address of the customer/company,
        /// generally the "invoice address" or "account holder" details.
        /// </summary>
        /// <remarks>
        /// Information contained within this parameter is required for
        /// certain payment methods, and if fraud screening is used.
        /// </remarks>
        public NameAndAddress BillTo { get; set; }

        /// <summary>
        /// Gets or sets the ship-to name and address of the recipient.
        /// </summary>
        /// <remarks>
        /// Information contained within this parameter is required for
        /// certain payment methods, and if fraud screening is used.
        /// </remarks>
        public NameAndAddress ShipTo { get; set; }

        /// <summary>
        /// Gets the line items included in this order (what the customer is paying for).
        /// You may include as many line items as required to specify the order.
        /// </summary>
        /// <remarks>
        /// It is highly recommended that you always send in complete line item information.
        /// Several payment methods, especially invoicing methods, require complete line
        /// information and you will not be able to use these methods if lines are not
        /// provided.
        /// </remarks>
        public LineItem[] LineItems
        {
            get { return InternalLineItems.ToArray(); }
        }

        /// <summary>
        /// Creates a request object used with <see cref="IPaynovaClient.CreateOrder(CreateOrderRequest)"/>
        /// to create an order within Paynova's system.
        /// </summary>
        /// <param name="orderNumber">
        /// Your identifier for the order, most likely from your order management system.
        /// </param>
        /// <param name="currencyCode"></param>
        /// <param name="totalAmount">
        /// The total amount of the order. If line items are provided,
        /// the value of this field must equal the total sum of all line items.
        /// <remarks>For Japanese Yuan (JPY), only whole numbers are allowed.</remarks>
        /// </param>
        public CreateOrderRequest(string orderNumber, CurrencyCode currencyCode, decimal totalAmount)
            : this(orderNumber, currencyCode.Alpha3, totalAmount) { }

        /// <summary>
        /// Creates a request object used with <see cref="IPaynovaClient.CreateOrder(CreateOrderRequest)"/>
        /// to create an order within Paynova's system.
        /// </summary>
        /// <param name="orderNumber">
        /// Your identifier for the order, most likely from your order management system.
        /// </param>
        /// <param name="currencyCode">Example: SEK, 752</param>
        /// <param name="totalAmount">
        /// The total amount of the order. If line items are provided,
        /// the value of this field must equal the total sum of all line items.
        /// <remarks>For Japanese Yuan (JPY), only whole numbers are allowed.</remarks>
        /// </param>
        public CreateOrderRequest(string orderNumber, string currencyCode, decimal totalAmount)
        {
            Ensure.That(orderNumber, "orderNumber").IsNotNullOrEmpty();
            Ensure.That(currencyCode, "currencyCode").IsNotNullOrEmpty();
            Ensure.That(totalAmount, "totalAmount").IsGt(0);

            OrderNumber = orderNumber;
            CurrencyCode = currencyCode;
            TotalAmount = totalAmount;

            InternalLineItems = new List<LineItem>();
        }

        public virtual CreateOrderRequest AddLineItem(LineItem item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalLineItems.Add(item);

            return this;
        }

        public virtual CreateOrderRequest ClearLineItems()
        {
            InternalLineItems.Clear();

            return this;
        }

        public virtual CreateOrderRequest WithLineItems(params LineItem[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }

        public virtual CreateOrderRequest WithLineItems(IEnumerable<LineItem> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }

        public virtual CreateOrderRequest WithBillTo(Action<NameAndAddress> config)
        {
            Ensure.That(config, "config").IsNotNull();

            var value = new NameAndAddress();

            config(value);

            BillTo = value;

            return this;
        }

        public virtual CreateOrderRequest WithShipTo(Action<NameAndAddress> config)
        {
            Ensure.That(config, "config").IsNotNull();

            var value = new NameAndAddress();

            config(value);

            ShipTo = value;

            return this;
        }

        public virtual CreateOrderRequest WithCustomer(Action<Customer> config)
        {
            Ensure.That(config, "config").IsNotNull();

            var value = new Customer();

            config(value);

            Customer = value;

            return this;
        }
    }
}