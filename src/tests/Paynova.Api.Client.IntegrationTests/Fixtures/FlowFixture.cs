using System;
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
        private readonly Testing.Lazy<GetAddressesRequest> _getAddressesRequest;
        private readonly Testing.Lazy<CreateOrderRequest> _createOrderRequest;
        private readonly Testing.Lazy<AuthorizeInvoiceRequest> _authorizeInvoiceRequest;
        private readonly Testing.Lazy<InitializePaymentRequest> _initializePaymentRequest;
        private readonly Testing.Lazy<FinalizeAuthorizationRequest> _finalizeAuthorizationRequest;
        private readonly Testing.Lazy<AnnulAuthorizationRequest> _annulAuthorizationRequest;

        public string OrderNumber { get; private set; }
        public GetAddressesRequest GetAddressesRequest => _getAddressesRequest.Instance;
        public CreateOrderRequest CreateOrderRequest => _createOrderRequest.Instance;
        public AuthorizeInvoiceRequest AuthorizeInvoiceRequest => _authorizeInvoiceRequest.Instance;
        public InitializePaymentRequest InitializePaymentRequest => _initializePaymentRequest.Instance;
        public FinalizeAuthorizationRequest FinalizeAuthorizationRequest => _finalizeAuthorizationRequest.Instance;
        public AnnulAuthorizationRequest AnnulAuthorizationRequest => _annulAuthorizationRequest.Instance;

        public GetAddressesResponse GetAddressesResponse { get; set; }
        public CreateOrderResponse CreateOrderResponse { get; set; }
        public AuthorizeInvoiceResponse AuthorizeInvoiceResponse { get; set; }
        public InitializePaymentResponse InitializePaymentResponse { get; set; }
        public FinalizeAuthorizationResponse FinalizeAuthorizationResponse { get; set; }

        public FlowFixture()
        {
            OrderNumber = Guid.NewGuid().ToString("n");
            _getAddressesRequest = new Testing.Lazy<GetAddressesRequest>(InitGetAddressesRequest);
            _createOrderRequest = new Testing.Lazy<CreateOrderRequest>(InitCreateOrderRequest);
            _authorizeInvoiceRequest = new Testing.Lazy<AuthorizeInvoiceRequest>(InitAuthorizeInvoiceRequest);
            _initializePaymentRequest = new Testing.Lazy<InitializePaymentRequest>(InitInitializePaymentRequest);
            _finalizeAuthorizationRequest = new Testing.Lazy<FinalizeAuthorizationRequest>(InitFinalizeAuthorizationRequest);
            _annulAuthorizationRequest = new Testing.Lazy<AnnulAuthorizationRequest>(InitAnnulAuthorizationRequest);
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
            return new GetAddressesRequest("SE", IntegrationTestsRuntime.Environment.CustomerGovernmentId);
        }

        protected virtual CreateOrderRequest InitCreateOrderRequest()
        {
            var address = GetAddressesResponse?.Addresses.FirstOrDefault(a => a.Address.Type == "legal");
            var lineItems = new[]
            {
                new LineItem(1, "MED-RED-BAL", "Medium red balloon.", "some product", 1, "ea.", 25m, 45, 11.25m, 56.25m,  null)
                {
                    Description = "Line item 1"
                },
                new LineItem(2, "MED-RED-BAL", "Medium red balloon.", "some product", 2, "ea.", 25m, 45, 22.50m, 112.50m, null)
                {
                    Description = "Line item 2"
                },
                new LineItem(3, "MED-RED-BAL", "Medium red balloon.", "some product", 1, "ea.", 25m, 45, 11.25m, 56.25m, null)
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
                    c.EmailAddress = IntegrationTestsRuntime.Environment.CustomerEmail;
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