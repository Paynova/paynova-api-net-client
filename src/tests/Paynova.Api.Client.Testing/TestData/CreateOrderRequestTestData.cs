using System.Linq;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;

namespace Paynova.Api.Client.Testing.TestData
{
    public static class CreateOrderRequestTestData
    {
        private const decimal TotalAmount = 112.53m;

        public static CreateOrderRequest CreateSimple(string orderNumber)
        {
            return CreateSimple(orderNumber, TotalAmount);
        }

        public static CreateOrderRequest CreateSimple(string orderNumber, decimal totalAmount)
        {
            return new CreateOrderRequest(orderNumber, CurrencyCode.SwedishKrona, totalAmount);
        }

        public static CreateOrderRequest CreateDetailedWithInvalidTotalAmount(string orderNumber)
        {
            var lineItems = LineItemTestData.CreateTravelLineItems();

            const decimal adjustTotalSumWithToMakeItInvalid = 2.25m;
            var request = CreateSimple(orderNumber, lineItems.Sum(i => i.TotalLineAmount) + adjustTotalSumWithToMakeItInvalid)
                .WithLineItems(lineItems);

            return request;
        }

        public static CreateOrderRequest CreateDetailedWithLineItems(string orderNumber, int numOfLineItems)
        {
            var lineItems = LineItemTestData.CreateLineItems(numOfLineItems);
            var request = CreateDetailedWithoutLineItems(orderNumber, lineItems.Sum(i => i.TotalLineAmount))
                .WithLineItems(lineItems);

            return request;
        }

        public static CreateOrderRequest CreateDetailedWithTravelLineItems(string orderNumber)
        {
            var lineItems = LineItemTestData.CreateTravelLineItems();
            var request = CreateDetailedWithoutLineItems(orderNumber, lineItems.Sum(i => i.TotalLineAmount))
                .WithLineItems(lineItems);

            return request;
        }

        private static CreateOrderRequest CreateDetailedWithoutLineItems(string orderNumber, decimal totalAmount)
        {
            return new CreateOrderRequest(orderNumber, CurrencyCode.SwedishKrona, totalAmount)
            {
                OrderDescription = "My order description",
                SalesChannel = "My sales channel",
                SalesLocationId = "My sales location id",
                Customer = new Customer
                {
                    CustomerId = "My customer id",
                    EmailAddress = "sample@mycustomer.com",
                    HomeTelephone = "+4681234567",
                    WorkTelephone = "+4682345678",
                    MobileTelephone = "+467099999999",
                    Name = new CompanyOrPersonName
                    {
                        FirstName = "John",
                        LastName = "Testsson",
                        MiddleNames = "Doe",
                        Suffix = "Sr.",
                        Title = "Mr."
                    }
                },
                BillTo = new NameAndAddress
                {
                    Name = new CompanyOrPersonName
                    {
                        CompanyName = "Test company"
                    },
                    Address = new Address
                    {
                        Street1 = "The company street 1",
                        Street2 = "The company street 2",
                        Street3 = "The company street 3",
                        Street4 = "The company street 4",
                        City = "Stockholm",
                        CountryCode = "SWE",
                        PostalCode = "12345",
                        RegionCode = "SL"
                    }
                },
                ShipTo = new NameAndAddress
                {
                    Name = new CompanyOrPersonName
                    {
                        FirstName = "John",
                        LastName = "Testsson",
                        MiddleNames = "",
                        Suffix = "Sr.",
                        Title = "Mr."
                    },
                    Address = new Address
                    {
                        Street1 = "The customer street 1",
                        Street2 = "The customer street 2",
                        Street3 = "The customer street 3",
                        Street4 = "The customer street 4",
                        City = "Gothenburg",
                        CountryCode = "SWE",
                        PostalCode = "54321",
                        RegionCode = "VG"
                    }
                }
            };
        }
    }
}