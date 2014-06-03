using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    /// <summary>
    /// Represents the request used to create a payment Session within Paynova's system.
    /// <![CDATA[http://docs.paynova.com]]>
    /// </summary>
    public class InitializePaymentRequest : Request
    {
        protected List<PaymentMethod> InternalPaymentMethods { get; private set; }
        protected List<CustomDataField> InternalCustomData { get; private set; }
        protected List<LineItem> InternalLineItems { get; private set; }

        /// <summary>
        /// Gets the unique identifier that you received from Paynova
        /// in the response from <see cref="IPaynovaClient.CreateOrder(CreateOrderRequest)"/>.
        /// </summary>
        public Guid OrderId { get; private set; }

        /// <summary>
        /// Gets the total amount that should be processed for this payment.
        /// This must be equal to or less than the original order amount
        /// and less than the original order amount minus any payments
        /// which have been already made on the order.
        /// </summary>
        /// <remarks>
        /// For the majority of implementors, this will always be equal to
        /// the original amount. Lesser amounts would typically only be used
        /// in the use case where multiple payments are being made on the
        /// same order.
        /// </remarks>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Gets the id for the payment channel.
        /// </summary>
        public int PaymentChannelId { get; private set; }

        /// <summary>
        /// Gets the collection of payment methods to display to the customer
        /// If you do not send a value, all configured and available payment methods
        /// will be displayed.
        /// </summary>
        public PaymentMethod[] PaymentMethods { get { return InternalPaymentMethods.ToArray(); } }

        /// <summary>
        /// Gets the collection of private key-value data fields to store as meta-data
        /// on the transaction. This data can be viewed in Merchant Services and is returned
        /// in the GetOrder response object. We do not use this data for processing transactions.
        /// </summary>
        public CustomDataField[] CustomData { get { return InternalCustomData.ToArray(); } }

        /// <summary>
        /// Gets or sets the time-to-live (TTL) of the session, in seconds, before the session times out.
        /// </summary>
        public int? SessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the name or id of the routing table to use when processing payments.
        /// </summary>
        /// <remarks>
        /// This field is used for advanced configurations and should only be provided if asked
        /// to do so by Paynova. Providing an invalid value in this field may result in payment
        /// failures.
        /// </remarks>
        public string RoutingIndicator { get; set; }

        /// <summary>
        /// Gets or sets the name or id of the fraud screening profile (named set of fraud screening rules) to use.
        /// </summary>
        /// <remarks>
        /// This field is used for advanced configurations and should only be provided if asked
        /// to do so by Paynova. Providing an invalid or incorrect value in this field may result in
        /// transactions being incorrectly accepted/declined by the fraud screening provider.
        /// </remarks>
        public string FraudScreeningProfile { get; set; }

        /// <summary>
        /// Used to configure the rendering of the interface.
        /// </summary>
        public InterfaceOptions InterfaceOptions { get; private set; }

        /// <summary>
        /// Gets or sets the profile payment options.
        /// </summary>
        public ProfilePaymentOptions ProfilePaymentOptions { get; set; }

        /// <summary>
        /// Gets the line items included with this payment.
        /// </summary>
        /// <remarks>
        /// NOT supported if the order was created as a simple order.
        /// It is highly recommended that you always send in complete line item information.
        /// Several payment methods, especially invoicing methods, require complete line
        /// information and you will not be able to use these methods if lines are not
        /// provided.
        /// </remarks>
        public LineItem[] LineItems { get { return InternalLineItems.ToArray(); } }

        /// <summary>
        /// Creates a request object used with <see cref="IPaynovaClient.InitializePayment"/>
        /// to create a payment Session within Paynova's system.
        /// </summary>
        /// <param name="orderId">
        /// The unique identifier (GUID) that you received from Paynova
        /// in the response from <see cref="IPaynovaClient.CreateOrder(CreateOrderRequest)"/>.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount that should be processed for this payment.
        /// This must be equal to or less than the original order amount
        /// and less than the original order amount minus any payments
        /// which have been already made on the order.
        /// For the majority of implementors, this will always be equal to
        /// the original amount. Lesser amounts would typically only be used
        /// in the use case where multiple payments are being made on the
        /// same order.
        /// </param>
        /// <param name="paymentChannelId">
        /// The channel of payment.
        /// Using helper factory method on <see cref="Model.PaymentChannelId"/>:
        /// PaymentChannelId.Web()
        /// Or if not yet added to helpers:
        /// new PaymentChannelId(1)
        /// </param>
        /// <param name="interfaceOptions">Used to configure the rendering of the interface.</param>
        public InitializePaymentRequest(Guid orderId, decimal totalAmount, PaymentChannelId paymentChannelId, InterfaceOptions interfaceOptions)
        {
            Ensure.That(orderId, "orderId").IsNotEmpty();
            Ensure.That(totalAmount, "totalAmount").IsGt(0);
            Ensure.That(interfaceOptions, "interfaceOptions").IsNotNull();

            OrderId = orderId;
            TotalAmount = totalAmount;
            PaymentChannelId = paymentChannelId;
            InterfaceOptions = interfaceOptions;

            InternalPaymentMethods = new List<PaymentMethod>();
            InternalCustomData = new List<CustomDataField>();
            InternalLineItems = new List<LineItem>();
        }

        public virtual InitializePaymentRequest AddPaymentMethod(PaymentMethod item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalPaymentMethods.Add(item);

            return this;
        }

        public virtual InitializePaymentRequest ClearPaymentMethods()
        {
            InternalPaymentMethods.Clear();

            return this;
        }

        public virtual InitializePaymentRequest WithPaymentMethods(params PaymentMethod[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalPaymentMethods.Clear();
            InternalPaymentMethods.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest WithPaymentMethods(IEnumerable<PaymentMethod> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalPaymentMethods.Clear();
            InternalPaymentMethods.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest AddCustomData(CustomDataField item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalCustomData.Add(item);

            return this;
        }

        public virtual InitializePaymentRequest ClearCustomData()
        {
            InternalCustomData.Clear();

            return this;
        }

        public virtual InitializePaymentRequest WithCustomData(params CustomDataField[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalCustomData.Clear();
            InternalCustomData.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest WithCustomData(IEnumerable<CustomDataField> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalCustomData.Clear();
            InternalCustomData.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest AddLineItem(LineItem item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalLineItems.Add(item);

            return this;
        }

        public virtual InitializePaymentRequest ClearLineItems()
        {
            InternalLineItems.Clear();

            return this;
        }

        public virtual InitializePaymentRequest WithLineItems(params LineItem[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest WithLineItems(IEnumerable<LineItem> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalLineItems.Clear();
            InternalLineItems.AddRange(items);

            return this;
        }

        public virtual InitializePaymentRequest WithSessionTimeout(int value)
        {
            SessionTimeout = value;

            return this;
        }

        public virtual InitializePaymentRequest WithRoutingIndicator(string value)
        {
            RoutingIndicator = value;

            return this;
        }

        public virtual InitializePaymentRequest WithFraudScreeningProfile(string value)
        {
            FraudScreeningProfile = value;

            return this;
        }
    }
}