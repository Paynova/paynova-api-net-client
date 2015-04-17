using System;
using System.Configuration;
using System.Linq;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.IntegrationTests.Fixtures
{
    public class FlowFixture : IResetableFixture
    {
        private readonly Lazy<GetAddressesRequest> _getAddressesRequest;
        private readonly Lazy<CreateOrderRequest> _createOrderRequest;
        private readonly Lazy<AuthorizeInvoiceRequest> _authorizeInvoiceRequest;
        private readonly Lazy<InitializePaymentRequest> _initializePaymentRequest;
        private readonly Lazy<FinalizeAuthorizationRequest> _finalizeAuthorizationRequest;
        private readonly Lazy<AnnulAuthorizationRequest> _annulAuthorizationRequest;

        public string OrderNumber { get; private set; }
        public GetAddressesRequest GetAddressesRequest { get { return _getAddressesRequest.Instance; } }
        public CreateOrderRequest CreateOrderRequest { get { return _createOrderRequest.Instance; } }
        public AuthorizeInvoiceRequest AuthorizeInvoiceRequest { get { return _authorizeInvoiceRequest.Instance; } }
        public InitializePaymentRequest InitializePaymentRequest { get { return _initializePaymentRequest.Instance; } }
        public FinalizeAuthorizationRequest FinalizeAuthorizationRequest { get { return _finalizeAuthorizationRequest.Instance; } }
        public AnnulAuthorizationRequest AnnulAuthorizationRequest { get { return _annulAuthorizationRequest.Instance; } }

        public GetAddressesResponse GetAddressesResponse { get; set; }
        public CreateOrderResponse CreateOrderResponse { get; set; }
        public AuthorizeInvoiceResponse AuthorizeInvoiceResponse { get; set; }
        public InitializePaymentResponse InitializePaymentResponse { get; set; }
        public FinalizeAuthorizationResponse FinalizeAuthorizationResponse { get; set; }

        public FlowFixture()
        {
            OrderNumber = Guid.NewGuid().ToString("n");
            _getAddressesRequest = new Lazy<GetAddressesRequest>(InitGetAddressesRequest);
            _createOrderRequest = new Lazy<CreateOrderRequest>(InitCreateOrderRequest);
            _authorizeInvoiceRequest = new Lazy<AuthorizeInvoiceRequest>(InitAuthorizeInvoiceRequest);
            _initializePaymentRequest = new Lazy<InitializePaymentRequest>(InitInitializePaymentRequest);
            _finalizeAuthorizationRequest = new Lazy<FinalizeAuthorizationRequest>(InitFinalizeAuthorizationRequest);
            _annulAuthorizationRequest = new Lazy<AnnulAuthorizationRequest>(InitAnnulAuthorizationRequest);
        }

        public void Reset()
        {
            _getAddressesRequest.Reset();
            _createOrderRequest.Reset();
            _authorizeInvoiceRequest.Reset();
            _initializePaymentRequest.Reset();
            _finalizeAuthorizationRequest.Reset();
            _annulAuthorizationRequest.Reset();
        }

        protected virtual GetAddressesRequest InitGetAddressesRequest()
        {
            return new GetAddressesRequest("SE", ConfigurationManager.AppSettings["customer_governmentId"]);
        }

        protected virtual CreateOrderRequest InitCreateOrderRequest()
        {
            var address = GetAddressesResponse != null
                ? GetAddressesResponse.Addresses.FirstOrDefault(a => a.Address.Type == "legal")
                : null;
            var lineItems = new[]
            {
                new LineItem(1, "MED-RED-BAL", "Medium red balloon.", "ea.", 25m, 1, 45, 56.25m, 11.25m)
                {
                    Description = "Line item 1"
                },
                new LineItem(2, "MED-RED-BAL", "Medium red balloon.", "ea.", 25m, 2, 45, 112.50m, 22.50m)
                {
                    Description = "Line item 2"
                },
                new LineItem(3, "MED-RED-BAL", "Medium red balloon.", "ea.", 25m, 1, 45, 56.25m, 11.25m)
                {
                    Description = "Line item 3"
                }
            };
            var request = new CreateOrderRequest(OrderNumber, CurrencyCode.SwedishKrona, lineItems.Sum(i => i.TotalLineAmount))
            {
                OrderDescription = ".Net SDK Test"
            }.WithLineItems(lineItems);

            if (address == null)
                return request;

            return request
                .WithCustomer(c =>
                {
                    c.GovernmentId = GetAddressesResponse.GovernmentId;
                    c.EmailAddress = ConfigurationManager.AppSettings["customer_email"];
                    c.Name.FirstName = address.Name.FirstName;
                    c.Name.LastName = address.Name.LastName;
                })
                .WithShipAndBillTo(address);
        }

        protected virtual AuthorizeInvoiceRequest InitAuthorizeInvoiceRequest()
        {
            return new AuthorizeInvoiceRequest(
                CreateOrderResponse.OrderId,
                CreateOrderRequest.TotalAmount,
                PaymentMethod.PaynovaInvoice,
                "DirectInvoice",
                PaymentChannelId.Web);
        }

        protected virtual InitializePaymentRequest InitInitializePaymentRequest()
        {
            var interfaceOptions = new InterfaceOptions(
                InterfaceId.Aero,
                "SWE",
                "https://foo.com/payments/success".ToUri(),
                "https://foo.com/payments/cancel".ToUri(),
                "https://foo.com/payments/pending".ToUri());

            var r = new InitializePaymentRequest(CreateOrderResponse.OrderId, CreateOrderRequest.TotalAmount, PaymentChannelId.Web, interfaceOptions);

            return CreateOrderRequest.LineItems.Any()
                ? r.WithLineItems(CreateOrderRequest.LineItems)
                : r;
        }

        protected virtual FinalizeAuthorizationRequest InitFinalizeAuthorizationRequest()
        {
            return new FinalizeAuthorizationRequest(AuthorizeInvoiceResponse.TransactionId, AuthorizeInvoiceRequest.OrderId, AuthorizeInvoiceRequest.TotalAmount);
        }

        protected virtual AnnulAuthorizationRequest InitAnnulAuthorizationRequest()
        {
            return new AnnulAuthorizationRequest(AuthorizeInvoiceResponse.TransactionId, AuthorizeInvoiceRequest.OrderId, AuthorizeInvoiceRequest.TotalAmount);
        }
    }
}