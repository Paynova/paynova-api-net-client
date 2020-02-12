using System;
using System.Collections.Generic;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Testing.TestData
{
    public static class LineItemTestData
    {
        public static LineItem CreateLineItem(int n, string articleName, string groupKey)
        {
            const decimal fakeUnitAmountExcludingTax = 2.25m;
            const decimal fakeTaxPercent = 0.25m;

            var totalLineAmount = (fakeUnitAmountExcludingTax * n) * (1 + fakeTaxPercent);
            var totalLineTaxAmount = (fakeUnitAmountExcludingTax * n) * fakeTaxPercent;

            return new LineItem("id:" + n, "num:" + n, articleName + n, "Some product", n, "st", fakeUnitAmountExcludingTax, fakeTaxPercent, totalLineTaxAmount, totalLineAmount, null)
            {
                Description = "A nice little " + articleName + " " + n,
                ProductUrl = "http://foo.com/articles/" + n,
                LineItemGroupKey = groupKey
            };
        }

        public static List<LineItem> CreateLineItems(int numOfLineItems)
        {
            var lineItems = new List<LineItem>();

            for (var n = 1; n <= numOfLineItems; n++)
                lineItems.Add(CreateLineItem(n, "Some article", (n % 2 == 0) ? "EVEN" : "ODD"));

            return lineItems;
        } 

        public static List<LineItem> CreateTravelLineItems()
        {
            return new List<LineItem>
            {
                CreateAirTravelLineItem(1, "Flight - A to B"),
                CreateRailTravelLineItem(2, "Train - A to B")
            };
        }

        private static LineItem CreateAirTravelLineItem(int n, string articleName)
        {
            var travelSegment = new AirTravelSegment(
                new DateTime(2014, 5, 3, 11, 35, 0), new DateTime(2014, 5, 3, 13, 35, 0),
                "SWE", "SWE",
                "IATA:ARN", "IATA:GOT",
                "UIC:1174") //TODO: Switch for "IATA:SK" when platform has fixed bug.
                .AddTicket(new TravelTicket(true, true)
                {
                    ServiceId = "SK1427",
                    TicketNumber = "2xgfrt5",
                    Passenger = new TravelPassenger
                    {
                        EmailAddress = "none@foo.com",
                        Telephone = "+46 000 000 00",
                        Name = new CompanyOrPersonName
                        {
                            FirstName = "John",
                            LastName = "Testsson",
                            MiddleNames = "Doe",
                            Suffix = "Sr.",
                            Title = "Mr."
                        }
                    }
                });

            var item = CreateLineItem(n, articleName, "Air");
            item.TravelData = new TravelData("Booking ref " + n, travelSegment);

            return item;
        }

        private static LineItem CreateRailTravelLineItem(int n, string articleName)
        {
            var travelSegment = new RailTravelSegment(
                new DateTime(2014, 5, 3, 11, 35, 0), new DateTime(2014, 5, 3, 13, 35, 0),
                "SWE", "SWE",
                "ESTOCOLMO CITY", "GBG CITY",
                "UIC:1174")
            .AddTicket(new TravelTicket(true, true)
            {
                ServiceId = "SK1427",
                TicketNumber = "2xgfrt5",
                Passenger = new TravelPassenger
                {
                    EmailAddress = "none@foo.com",
                    Telephone = "+46 000 000 00",
                    Name = new CompanyOrPersonName
                    {
                        FirstName = "John",
                        LastName = "Testsson",
                        MiddleNames = "Doe",
                        Suffix = "Sr.",
                        Title = "Mr."
                    }
                }
            });

            var item = CreateLineItem(n, articleName, "Rail");
            item.TravelData = new TravelData("Booking ref " + n, travelSegment);

            return item;
        }
    }
}