using System;

namespace Paynova.Api.Client.Responses
{
    public class AuthorizeInvoiceResponse : Response
    {
        /// <summary>
        /// Paynovas unique order id associated with this transaction.
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Paynovas unique id associated with this transaction.
        /// This id must be used in subsequent requests to Finalize Authorization and Annul Authorization.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Id of the settling part
        /// </summary>
        public int AcquirerId { get; set; }

        /// <summary>
        /// Reference Id from the settling part
        /// </summary>
        public string AcquirerReferenceId { get; set; }
    }
}