using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;

namespace Paynova.Api.Client.IntegrationTests
{
    internal static class TestDataExtensionMethods
    {
        internal static CreateOrderRequest AddExtraLineItemWithoutUpdatingTotalAmount(this CreateOrderRequest request)
        {
            return request.AddLineItem(CreateExtraLineItem());
        }

        internal static InitializePaymentRequest AddExtraLineItemWithoutUpdatingTotalAmount(this InitializePaymentRequest request)
        {
            return request.AddLineItem(CreateExtraLineItem());
        }

        private static LineItem CreateExtraLineItem()
        {
            return new LineItem(
                "extra",
                "extra",
                "Extra line failing order total amount",
                "some product",
                1,
                "ea.",
                25,
                10m,
                2.5m,
                12.5m,
                null);
        }
    }
}