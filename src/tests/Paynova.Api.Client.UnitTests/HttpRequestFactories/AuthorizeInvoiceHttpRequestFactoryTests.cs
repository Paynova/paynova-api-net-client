using System;
using Paynova.Api.Client.HttpRequestFactories;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.UnitTests.HttpRequestFactories
{
    public class AuthorizeInvoiceHttpRequestFactoryTests : UnitTestsOf<AuthorizeInvoiceHttpRequestFactory>
    {
        private const decimal TotalAmount = 125.50m;
        private readonly Guid _orderId ;

        public AuthorizeInvoiceHttpRequestFactoryTests()
        {
            _orderId = new Guid("499de2b4-e55e-433b-bf33-fa39efc0ce68");

            SUT = new AuthorizeInvoiceHttpRequestFactory(Runtime, Serializer);
        }

        [MyFact]
        public void Can_create_http_request()
        {
            var request = new AuthorizeInvoiceRequest(_orderId, TotalAmount, PaymentMethod.PaynovaInvoice, "DirectInvoice", PaymentChannelId.Web);

            var httpRequest = SUT.Create(request);

            httpRequest.ShouldBe().PostAgainst(
                "/orders/{0}/authorizePayment",
                _orderId.ToString("n"));

            httpRequest.ShouldBe().PostWithJson(ExpectedJson.AuthorizeInvoice);
        }
    }
}