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
        protected IHttpRequestFactory<AuthorizeInvoiceRequest> AuthorizeInvoiceHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<InitializePaymentRequest> InitializePaymentHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<RefundPaymentRequest> RefundPaymentHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<FinalizeAuthorizationRequest> FinalizeAuthorizationHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<AnnulAuthorizationRequest> AnnulAuthorizationHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<GetAddressesRequest> GetAddressesHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<GetCustomerProfileRequest> GetCustomerProfileHttpRequestFactory { get; private set; }
        protected IHttpRequestFactory<GetPaymentOptionsRequest> GetPaymentOptionsHttpRequestFactory { get; private set; }
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
            AuthorizeInvoiceHttpRequestFactory = new AuthorizeInvoiceHttpRequestFactory(Runtime.Instance, Serializer);
            InitializePaymentHttpRequestFactory = new InitializePaymentHttpRequestFactory(Runtime.Instance, Serializer);
            RefundPaymentHttpRequestFactory = new RefundPaymentHttpRequestFactory(Runtime.Instance, Serializer);
            FinalizeAuthorizationHttpRequestFactory = new FinalizeAuthorizationHttpRequestFactory(Runtime.Instance, Serializer);
            AnnulAuthorizationHttpRequestFactory = new AnnulAuthorizationHttpRequestFactory(Runtime.Instance, Serializer);
            GetAddressesHttpRequestFactory = new GetAddressesHttpRequestFactory(Runtime.Instance, Serializer);
            GetCustomerProfileHttpRequestFactory = new GetCustomerProfileHttpRequestFactory(Runtime.Instance, Serializer);
            GetPaymentOptionsHttpRequestFactory = new GetPaymentOptionsHttpRequestFactory(Runtime.Instance, Serializer);
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

            var httpRequest = CreateOrderHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<CreateOrderResponse>(httpResponse);
        }

        public virtual InitializePaymentResponse InitializePayment(InitializePaymentRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = InitializePaymentHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<InitializePaymentResponse>(httpResponse);
        }

        public virtual RefundPaymentResponse RefundPayment(RefundPaymentRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = RefundPaymentHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<RefundPaymentResponse>(httpResponse);
        }

        public virtual AuthorizeInvoiceResponse AuthorizeInvoice(AuthorizeInvoiceRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = AuthorizeInvoiceHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<AuthorizeInvoiceResponse>(httpResponse);
        }

        public virtual FinalizeAuthorizationResponse FinalizeAuthorization(FinalizeAuthorizationRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = FinalizeAuthorizationHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<FinalizeAuthorizationResponse>(httpResponse);
        }

        public virtual void AnnulAuthorization(AnnulAuthorizationRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = AnnulAuthorizationHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            ResponseFactory.Create<AnnulAuthorizationResponse>(httpResponse);
        }

        public virtual GetAddressesResponse GetAddresses(string countryCode, string governmentId)
        {
            return GetAddresses(new GetAddressesRequest(countryCode, governmentId));
        }

        public virtual GetAddressesResponse GetAddresses(GetAddressesRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = GetAddressesHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<GetAddressesResponse>(httpResponse);
        }

        public virtual GetCustomerProfileResponse GetCustomerProfile(string profileId)
        {
            return GetCustomerProfile(new GetCustomerProfileRequest(profileId));
        }

        public virtual GetCustomerProfileResponse GetCustomerProfile(GetCustomerProfileRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = GetCustomerProfileHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<GetCustomerProfileResponse>(httpResponse);
        }

        public virtual GetPaymentOptionsResponse GetPaymentOptions(GetPaymentOptionsRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = GetPaymentOptionsHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            return ResponseFactory.Create<GetPaymentOptionsResponse>(httpResponse);
        }

        public virtual void RemoveCustomerProfile(string profileId)
        {
            RemoveCustomerProfile(new RemoveCustomerProfileRequest(profileId));
        }

        public virtual void RemoveCustomerProfile(RemoveCustomerProfileRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = RemoveCustomerProfileHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            ResponseFactory.Create<RemoveCustomerProfileResponse>(httpResponse);
        }

        public virtual void RemoveCustomerProfileCard(string profileId, Guid cardId)
        {
            RemoveCustomerProfileCard(new RemoveCustomerProfileCardRequest(profileId, cardId));
        }

        public virtual void RemoveCustomerProfileCard(RemoveCustomerProfileCardRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = RemoveCustomerProfileCardHttpRequestFactory.Create(request);
            var httpResponse = Connection.Send(httpRequest);

            ResponseFactory.Create<RemoveCustomerProfileCardResponse>(httpResponse);
        }
    }
}