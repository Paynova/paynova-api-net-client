using System;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client
{
    public class PaynovaClient : IPaynovaClient
    {
        public IHttpConnection Connection { get; private set; }
        public ISerializer Serializer { get; private set; }

        protected IHttpRequestFactory<CreateOrderRequest> CreateOrderHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<InitializePaymentRequest> InitializePaymentHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<RefundPaymentRequest> RefundPaymentHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<FinalizeAuthorizationRequest> FinalizeAuthorizationHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<AnnulAuthorizationRequest> AnnulAuthorizationHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<GetCustomerProfileRequest> GetCustomerProfileHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<RemoveCustomerProfileCardRequest> RemoveCustomerProfileCardHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<RemoveCustomerProfileRequest> RemoveCustomerProfileHttpRequestFactory { get; private set; }

        protected IResponseFactory ResponseFactory { get; private set; }

        public PaynovaClient(string serverUrl, string username, string password)
            : this(new BasicHttpConnection(serverUrl, username, password))
        {
        }

        public PaynovaClient(IHttpConnection connection)
        {
            Ensure.That(connection, "connection").IsNotNull();

            Connection = connection;
            Serializer = new DefaultJsonSerializer();

            CreateOrderHttpRequestFactory = new CreateOrderHttpRequestFactory(Runtime.Instance, Serializer);
            InitializePaymentHttpRequestFactory = new InitializePaymentHttpRequestFactory(Runtime.Instance, Serializer);
            RefundPaymentHttpRequestFactory = new RefundPaymentHttpRequestFactory(Runtime.Instance, Serializer);
            FinalizeAuthorizationHttpRequestFactory = new FinalizeAuthorizationHttpRequestFactory(Runtime.Instance, Serializer);
            AnnulAuthorizationHttpRequestFactory = new AnnulAuthorizationHttpRequestFactory(Runtime.Instance, Serializer);
            GetCustomerProfileHttpRequestFactory = new GetCustomerProfileHttpRequestFactory(Runtime.Instance, Serializer);
            RemoveCustomerProfileCardHttpRequestFactory = new RemoveCustomerProfileCardHttpRequestFactory(Runtime.Instance, Serializer);
            RemoveCustomerProfileHttpRequestFactory = new RemoveCustomerProfileHttpRequestFactory(Runtime.Instance, Serializer);

            ResponseFactory = new GenericResponseFactory(Serializer);
        }

        public virtual CreateOrderResponse CreateOrder(string orderNumber, CurrencyCode currencyCode, decimal totalAmount)
        {
            return CreateOrder(new CreateOrderRequest(orderNumber, currencyCode, totalAmount));
        }

        public virtual CreateOrderResponse CreateOrder(string orderNumber, string currencyCode, decimal totalAmount)
        {
            return CreateOrder(new CreateOrderRequest(orderNumber, currencyCode, totalAmount));
        }

        public virtual CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            return ProcessCreateOrderHttpResponse(httpResponse);
        }

        public virtual InitializePaymentResponse InitializePayment(InitializePaymentRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            return ProcessInitializePaymentHttpResponse(httpResponse);
        }

        public virtual RefundPaymentResponse RefundPayment(RefundPaymentRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            return ProcessRefundPaymentHttpResponse(httpResponse);
        }

        public virtual FinalizeAuthorizationResponse FinalizeAuthorization(FinalizeAuthorizationRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            return ProcessFinalizeAuthorizationHttpResponse(httpResponse);
        }

        public virtual void AnnulAuthorization(AnnulAuthorizationRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            ProcessAnnulAuthorizationHttpResponse(httpResponse);
        }

        public virtual GetCustomerProfileResponse GetCustomerProfile(string profileId)
        {
            return GetCustomerProfile(new GetCustomerProfileRequest(profileId));
        }

        public virtual GetCustomerProfileResponse GetCustomerProfile(GetCustomerProfileRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            return ProcessGetCustomerProfileHttpResponse(httpResponse);
        }

        public virtual void RemoveCustomerProfile(string profileId)
        {
            RemoveCustomerProfile(new RemoveCustomerProfileRequest(profileId));
        }

        public virtual void RemoveCustomerProfile(RemoveCustomerProfileRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            ProcessRemoveCustomerProfileHttpResponse(httpResponse);
        }

        public virtual void RemoveCustomerProfileCard(string profileId, Guid cardId)
        {
            RemoveCustomerProfileCard(new RemoveCustomerProfileCardRequest(profileId, cardId));
        }

        public virtual void RemoveCustomerProfileCard(RemoveCustomerProfileCardRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateHttpRequest(request);
            var httpResponse = Connection.Send(httpRequest);

            ProcessRemoveCustomerProfileCardHttpResponse(httpResponse);
        }

        protected virtual HttpRequest CreateHttpRequest(CreateOrderRequest request)
        {
            return CreateOrderHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(InitializePaymentRequest request)
        {
            return InitializePaymentHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(RefundPaymentRequest request)
        {
            return RefundPaymentHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(FinalizeAuthorizationRequest request)
        {
            return FinalizeAuthorizationHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(AnnulAuthorizationRequest request)
        {
            return AnnulAuthorizationHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(GetCustomerProfileRequest request)
        {
            return GetCustomerProfileHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(RemoveCustomerProfileCardRequest request)
        {
            return RemoveCustomerProfileCardHttpRequestFactory.Create(request);
        }

        protected virtual HttpRequest CreateHttpRequest(RemoveCustomerProfileRequest request)
        {
            return RemoveCustomerProfileHttpRequestFactory.Create(request);
        }

        protected virtual CreateOrderResponse ProcessCreateOrderHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<CreateOrderResponse>(httpResponse);
        }

        protected virtual InitializePaymentResponse ProcessInitializePaymentHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<InitializePaymentResponse>(httpResponse);
        }

        protected virtual RefundPaymentResponse ProcessRefundPaymentHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<RefundPaymentResponse>(httpResponse);
        }

        protected virtual FinalizeAuthorizationResponse ProcessFinalizeAuthorizationHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<FinalizeAuthorizationResponse>(httpResponse);
        }

        protected virtual AnnulAuthorizationResponse ProcessAnnulAuthorizationHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<AnnulAuthorizationResponse>(httpResponse);
        }

        protected virtual GetCustomerProfileResponse ProcessGetCustomerProfileHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<GetCustomerProfileResponse>(httpResponse);
        }

        protected virtual RemoveCustomerProfileCardResponse ProcessRemoveCustomerProfileCardHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<RemoveCustomerProfileCardResponse>(httpResponse);
        }

        protected virtual RemoveCustomerProfileResponse ProcessRemoveCustomerProfileHttpResponse(HttpResponse httpResponse)
        {
            return ResponseFactory.Create<RemoveCustomerProfileResponse>(httpResponse);
        }
    }
}