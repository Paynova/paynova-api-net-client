using System;
using System.Linq;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.TestData;

namespace Paynova.Api.Client.IntegrationTests.OrderProcessTests
{
    public abstract class OrderProcessFixture
    {
        private readonly Lazy<CreateOrderRequest> _createOrderRequest;
        private readonly Lazy<InitializePaymentRequest> _initializePaymentRequest;

        protected string OrderNumber { get; private set; }
        public CreateOrderRequest CreateOrderRequest { get { return _createOrderRequest.Instance; } }
        public InitializePaymentRequest InitializePaymentRequest { get { return _initializePaymentRequest.Instance; } }
        public CreateOrderResponse CreateOrderResponse { get; set; }
        public InitializePaymentResponse InitializePaymentResponse { get; set; }

        protected OrderProcessFixture()
        {
            OrderNumber = Guid.NewGuid().ToString("n");
            _createOrderRequest = new Lazy<CreateOrderRequest>(CreateCreateOrderRequest);
            _initializePaymentRequest = new Lazy<InitializePaymentRequest>(CreateInitializePaymentRequest);
        }

        protected abstract CreateOrderRequest CreateCreateOrderRequest();

        protected virtual InitializePaymentRequest CreateInitializePaymentRequest()
        {
            var r = InitializePaymentRequestTestData.CreateSimple(CreateOrderResponse.OrderId, CreateOrderRequest.TotalAmount);

            return CreateOrderRequest.LineItems.Any()
                ? r.WithLineItems(CreateOrderRequest.LineItems)
                : r;
        }
    }
}