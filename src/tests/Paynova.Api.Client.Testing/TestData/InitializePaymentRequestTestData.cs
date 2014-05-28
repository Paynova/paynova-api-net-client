using System;
using System.Linq;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;

namespace Paynova.Api.Client.Testing.TestData
{
    public static class InitializePaymentRequestTestData
    {
        private const decimal TotalAmount = 112.53m;

        public static InitializePaymentRequest CreateSimple(Guid orderId)
        {
            return CreateSimple(orderId, TotalAmount);
        }

        public static InitializePaymentRequest CreateSimple(Guid orderId, decimal totalAmount)
        {
            var interfaceOptions = new InterfaceOptions(
                InterfaceId.Aero,
                "SWE",
                "https://foo.com/payments/success".ToUri(),
                "https://foo.com/payments/cancel".ToUri(),
                "https://foo.com/payments/pending".ToUri());

            return new InitializePaymentRequest(orderId, totalAmount, PaymentChannelId.Web, interfaceOptions);
        }

        public static InitializePaymentRequest CreateDetailedWithInvalidTotalAmount(Guid orderId)
        {
            var lineItems = LineItemTestData.CreateTravelLineItems();

            const decimal adjustTotalSumWithToMakeItInvalid = 2.25m;
            var request = CreateSimple(orderId, lineItems.Sum(i => i.TotalLineAmount) + adjustTotalSumWithToMakeItInvalid)
                .WithLineItems(lineItems);

            return request;
        }

        public static InitializePaymentRequest CreateDetailedWithLineItems(Guid orderId, int numOfLineItems)
        {
            var lineItems = LineItemTestData.CreateLineItems(numOfLineItems);
            var request = CreateDetailedWithoutLineItems(orderId, lineItems.Sum(i => i.TotalLineAmount))
                .WithLineItems(lineItems);

            return request;
        }

        public static InitializePaymentRequest CreateDetailedWithTravelLineItems(Guid orderId)
        {
            var lineItems = LineItemTestData.CreateTravelLineItems();
            var request = CreateDetailedWithoutLineItems(orderId, lineItems.Sum(i => i.TotalLineAmount))
                .WithLineItems(lineItems);

            return request;
        }

        private static InitializePaymentRequest CreateDetailedWithoutLineItems(Guid orderId, decimal totalAmount)
        {
            var request = CreateSimple(orderId, totalAmount);

            request.SessionTimeout = 180;
            request.RoutingIndicator = "some indicator";
            request.FraudScreeningProfile = "some fraud profile";
            request.ProfilePaymentOptions = new ProfilePaymentOptions("My profile id")
            {
                DisplaySaveProfileCardOption = true,
                ProfileCard = new ProfileCard(new Guid("a245ba13-a18c-467a-a7fc-951352e5263d"))
                {
                    Cvc = "111"
                }
            };

            return request
                .WithCustomData(new CustomDataField("key1", "value1"), new CustomDataField("key2", "value2"))
                .WithPaymentMethods(PaymentMethod.Visa, PaymentMethod.MasterCard);
        }
    }
}