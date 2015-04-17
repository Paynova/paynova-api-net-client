using System;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client
{
    public interface IPaynovaClient
    {
        /// <summary>
        /// Gets the underlying connection used to communicate with the remote endpoint.
        /// </summary>
        IHttpConnection Connection { get; }

        /// <summary>
        /// Gets the serializer used to handle serialization and deserialization of
        /// requests and responses.
        /// </summary>
        ISerializer Serializer { get; }

        /// <summary>
        /// Creates a simple order (no line items etc).
        /// </summary>
        /// <remarks>
        /// This will limit your options later in the process.
        /// E.g. By not being able to use all payment methods.
        /// </remarks>
        /// <param name="orderNumber"></param>
        /// <param name="currencyCode"><see cref="CurrencyCode"/> for assistance with values.</param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        CreateOrderResponse CreateOrder(string orderNumber, string currencyCode, decimal totalAmount);

        /// <summary>
        /// Creates a simple order (no line items etc).
        /// </summary>
        /// <remarks>
        /// This will limit your options later in the process.
        /// E.g. By not being able to use all payment methods.
        /// </remarks>
        /// <param name="orderNumber"></param>
        /// <param name="currencyCode"></param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        CreateOrderResponse CreateOrder(string orderNumber, CurrencyCode currencyCode, decimal totalAmount);

        /// <summary>
        /// Used to create an order within Paynovas system.
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        CreateOrderResponse CreateOrder(CreateOrderRequest request);

        /// <summary>
        /// Used to create a payment Session within Paynovas system
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        InitializePaymentResponse InitializePayment(InitializePaymentRequest request);

        /// <summary>
        /// Used to used to refund a Payment within Paynovas system
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RefundPaymentResponse RefundPayment(RefundPaymentRequest request);

        /// <summary>
        /// Used to authorize an invoice payment, instead of using pay-page.
        /// The next step would be to Finalize it using <see cref="FinalizeAuthorization"/>.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AuthorizeInvoiceResponse AuthorizeInvoice(AuthorizeInvoiceRequest request);

        /// <summary>
        /// Finalize all or part of an authorization.
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        FinalizeAuthorizationResponse FinalizeAuthorization(FinalizeAuthorizationRequest request);

        /// <summary>
        /// Annul all or part of an authorization.
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <param name="request"></param>
        void AnnulAuthorization(AnnulAuthorizationRequest request);

        /// <summary>
        ///  Used to retrieve information about a merchant customer profile stored at Paynova.
        /// </summary>
        /// <param name="profileId">
        /// Your unique identifier for the customer profile stored at Paynova
        /// </param>
        /// <returns></returns>
        GetCustomerProfileResponse GetCustomerProfile(string profileId);

        /// <summary>
        ///  Used to retrieve information about a merchant customer profile stored at Paynova.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetCustomerProfileResponse GetCustomerProfile(GetCustomerProfileRequest request);

        /// <summary>
        /// Used to remove a customer profile and all related data.
        /// </summary>
        /// <param name="profileId"></param>
        void RemoveCustomerProfile(string profileId);

        /// <summary>
        /// Used to remove a customer profile and all related data.
        /// </summary>
        /// <param name="request"></param>
        void RemoveCustomerProfile(RemoveCustomerProfileRequest request);

        /// <summary>
        /// Used to remove a stored card from a customer profile.
        /// </summary>
        /// <param name="profileId">
        /// Your unique identifier for the customer profile stored at Paynova
        /// </param>
        /// <param name="cardId">
        /// Paynovas GUID identifier for the card associated with the customer profile
        /// </param>
        void RemoveCustomerProfileCard(string profileId, Guid cardId);

        /// <summary>
        /// Used to remove a stored card from a customer profile.
        /// </summary>
        /// <param name="request"></param>
        void RemoveCustomerProfileCard(RemoveCustomerProfileCardRequest request);
    }
}