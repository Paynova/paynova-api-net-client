using System;

namespace Paynova.Api.Client.Responses
{
    public class InitializePaymentResponse : Response
    {
        /// <summary>
        /// The unique identifier (GUID) for the payment session.
        /// This id will be used when opening/redirecting to our hosted payment pages.
        /// </summary>
        /// <remarks>
        /// This parameter will not be returned if the operation fails.
        /// </remarks>
        public Guid SessionId { get; set; }

        /// <summary>
        /// The prepared URL to our hosted payment page.
        /// </summary>
        /// <remarks>
        /// This parameter will not be returned if the operation fails.
        /// </remarks>
        public string Url { get; set; }
    }
}