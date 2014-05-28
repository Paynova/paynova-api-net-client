using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class RailTravelSegment : TravelSegment<RailTravelSegment>
    {
        public override string SegmentType
        {
            get { return "Rail"; }
        }

        /// <summary>
        /// The identifier for the rail station which this segment is departing from.
        /// </summary>
        public string DepartureStationCode { get; private set; }

        /// <summary>
        /// The identifier for the arrival rail station of this segment.
        /// </summary>
        public string ArrivalStationCode { get; private set; }

        public RailTravelSegment(DateTime departureAt, DateTime arrivalAt, string departureCountryCode, string arrivalCountryCode, string departureStationCode, string arrivalStationCode, string carrierDesignator)
            : base(departureAt, arrivalAt, departureCountryCode, arrivalCountryCode, carrierDesignator)
        {
            Ensure.That(departureStationCode, "departureStationCode").IsNotNullOrEmpty();
            Ensure.That(arrivalStationCode, "arrivalStationCode").IsNotNullOrEmpty();

            DepartureStationCode = departureStationCode;
            ArrivalStationCode = arrivalStationCode;
        }
    }
}