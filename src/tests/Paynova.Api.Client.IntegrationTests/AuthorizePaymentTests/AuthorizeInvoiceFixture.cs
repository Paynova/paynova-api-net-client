using System;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.IntegrationTests.AuthorizePaymentTests
{
    public abstract class AuthorizeInvoiceFixture
    {
        private readonly Lazy<CreateOrderRequest> _createOrderRequest;
        private readonly Lazy<AuthorizeInvoiceRequest> _authorizeInvoiceRequest;

        protected string OrderNumber { get; private set; }
        public CreateOrderRequest CreateOrderRequest { get { return _createOrderRequest.Instance; } }
        public AuthorizeInvoiceRequest AuthorizeInvoiceRequest { get { return _authorizeInvoiceRequest.Instance; } }
        public CreateOrderResponse CreateOrderResponse { get; set; }
        public AuthorizeInvoiceResponse AuthorizeInvoiceResponse { get; set; }

        protected AuthorizeInvoiceFixture()
        {
            OrderNumber = Guid.NewGuid().ToString("n");
            _createOrderRequest = new Lazy<CreateOrderRequest>(CreateCreateOrderRequest);
            _authorizeInvoiceRequest = new Lazy<AuthorizeInvoiceRequest>(CreateAuthorizeInvoiceRequest);
        }

        protected abstract CreateOrderRequest CreateCreateOrderRequest();

        protected abstract AuthorizeInvoiceRequest CreateAuthorizeInvoiceRequest();
    }
}