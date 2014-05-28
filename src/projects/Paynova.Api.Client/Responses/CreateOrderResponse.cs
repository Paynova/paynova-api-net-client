using System;

namespace Paynova.Api.Client.Responses
{
    public class CreateOrderResponse : Response
    {
        /// <summary>
        /// This field will contain the order id (GUID) which you can use in subsequent calls to the order API .
        /// </summary>
        public Guid OrderId { get; set; }
    }
}