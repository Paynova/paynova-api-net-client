using System;
using System.Linq;
using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class AirTravelSegmentTests : TravelSegmentUnitTests<AirTravelSegment>
    {
        public AirTravelSegmentTests()
        {
            SUT = new AirTravelSegment(new DateTime(2014, 5, 1, 23, 35, 0), new DateTime(2014, 5, 2, 7, 5, 0), "SWE", "FRO", "IATA:ARN", "IATA:GOT", "IATA:SK");
        }

        [Fact]
        public override void It_should_be_configured_with_correct_segment_type()
        {
            SUT.SegmentType.Should().Be("Air");
        }
    }

    public class RailTravelSegmentTests : TravelSegmentUnitTests<RailTravelSegment>
    {
        public RailTravelSegmentTests()
        {
            SUT = new RailTravelSegment(new DateTime(2014, 5, 1, 23, 35, 0), new DateTime(2014, 5, 2, 7, 5, 0), "SWE", "SWE", "ESTOCOLMO CITY", "GBG CITY", "UIC:1174");
        }

        [Fact]
        public override void It_should_be_configured_with_correct_segment_type()
        {
            SUT.SegmentType.Should().Be("Rail");
        }
    }

    public abstract class TravelSegmentUnitTests<T> : UnitTestsOf<T> where T : TravelSegment<T>
    {
        [Fact]
        public abstract void It_should_be_configured_with_correct_segment_type();

        [Fact]
        public void It_extracts_deptarture_date_time_correctly()
        {
            SUT.DepartureDate.Should().Be("2014-05-01");
            SUT.DepartureTime.Should().Be("23:35");
        }

        [Fact]
        public void It_extracts_arrival_date_time_correctly()
        {
            SUT.ArrivalDate.Should().Be("2014-05-02");
            SUT.ArrivalTime.Should().Be("07:05");
        }

        [Fact]
        public void When_add_of_line_items_It_should_add_lineItems_in_the_request()
        {
            SUT
                .AddTicket(new TravelTicket(true, true))
                .AddTicket(new TravelTicket(true, true));

            SUT.Tickets.Length.Should().Be(2);
        }

        [Fact]
        public void When_setting_line_items_It_should_overwrite_lineItems_in_the_request()
        {
            SUT
                .AddTicket(new TravelTicket(true, true))
                .AddTicket(new TravelTicket(true, true));

            SUT.WithTickets(new TravelTicket(false, false));

            SUT.Tickets.Single().IsRebookable.Should().Be(false);
        }

        [Fact]
        public void When_clearing_line_items_It_should_remove_all_lineItems_in_the_request()
        {
            SUT
                .AddTicket(new TravelTicket(true, true))
                .AddTicket(new TravelTicket(true, true));

            SUT.ClearTickets();

            SUT.Tickets.Should().BeEmpty();
        }
    }
}