using System;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Requests
{
    /// <summary>
    /// Represents the request used to authorize an invoice within Paynovas system.
    /// <![CDATA[http://docs.paynova.com]]>
    /// </summary>
    public class AuthorizeInvoiceRequest : Request
    {
        public Guid OrderId { get; private set; }

        public string AuthorizationType
        {
            get { return "InvoicePayment"; }
        }
        public decimal TotalAmount { get; private set; }
        public int PaymentMethodId { get; private set; }
        public string PaymentMethodProductId { get; private set; }
        public int PaymentChannelId { get; private set; }

        /// <summary>
        /// Creates a new instance of the request.
        /// </summary>
        /// <param name="orderId">
        /// The unique id you received from Paynova to identify the order.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount that should be processed for this payment. 
        /// </param>
        /// <param name="paymentMethod">
        /// The Id of the invoice payment method. E.g. 311 for PaynovaInvoice.
        /// Use <see cref="PaymentMethod"/> for additional values.</param>
        /// <param name="paymentMethodProductId">
        /// The payment method product Id for the specific payment type. E.g DirectInvoice.
        /// </param>
        /// <param name="paymentChannelId">
        /// The channel of payment.
        /// Using helper factory method on <see cref="Model.PaymentChannelId"/>:
        /// PaymentChannelId.Web()
        /// Or if not yet added to helpers:
        /// new PaymentChannelId(1)
        /// </param>
        public AuthorizeInvoiceRequest(Guid orderId, decimal totalAmount, PaymentMethod paymentMethod, string paymentMethodProductId, PaymentChannelId paymentChannelId)
            : this(orderId, totalAmount, paymentMethod.Id, paymentMethodProductId, paymentChannelId) { }

        /// <summary>
        /// Creates a new instance of the request.
        /// </summary>
        /// <param name="orderId">
        /// The unique id you received from Paynova to identify the order.
        /// </param>
        /// <param name="totalAmount">
        /// The total amount that should be processed for this payment. 
        /// </param>
        /// <param name="paymentMethodId">
        /// The Id of the invoice payment method. E.g. 311 for PaynovaInvoice.
        /// Use <see cref="PaymentMethod"/> for additional values.</param>
        /// <param name="paymentMethodProductId">
        /// The payment method product Id for the specific payment type. E.g DirectInvoice.
        /// </param>
        /// <param name="paymentChannelId">
        /// The channel of payment.
        /// Using helper factory method on <see cref="Model.PaymentChannelId"/>:
        /// PaymentChannelId.Web()
        /// Or if not yet added to helpers:
        /// new PaymentChannelId(1)
        /// </param>
        public AuthorizeInvoiceRequest(Guid orderId, decimal totalAmount, int paymentMethodId, string paymentMethodProductId, PaymentChannelId paymentChannelId)
        {
            Ensure.That(orderId, "orderId").IsNotEmpty();
            Ensure.That(totalAmount, "totalAmount").IsGt(0);
            Ensure.That(paymentMethodId, "paymentMethodId").IsGt(0);
            Ensure.That(paymentMethodProductId, "paymentMethodProductId").IsNotNullOrEmpty();

            OrderId = orderId;
            TotalAmount = totalAmount;
            PaymentMethodId = paymentMethodId;
            PaymentMethodProductId = paymentMethodProductId;
            PaymentChannelId = paymentChannelId;
        }
    }
}