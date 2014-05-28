using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class AirTravelSegment : TravelSegment<AirTravelSegment>
    {
        public override string SegmentType
        {
            get { return "Air"; }
        }

        /// <summary>
        /// The IATA or ICAO airport or city code of the departure airport for this segment, in the format "authority:code".
        /// Example: for Arlanda airport in Stockholm, Sweden:
        /// For IATA you would send: "IATA:ARN"
        /// For ICAO you would send: "ICAO:ESSA"
        /// </summary>
        public string DepartureAirportCode { get; private set; }

        /// <summary>
        /// The IATA or ICAO airport or city code of the arrival airport for this segment, in the format "authority:code".
        /// Example: for Arlanda airport in Stockholm, Sweden:
        /// For IATA you would send: "IATA:ARN"
        /// For ICAO you would send: "ICAO:ESSA"
        /// </summary>
        public string ArrivalAirportCode { get; private set; }

        public AirTravelSegment(DateTime departureAt, DateTime arrivalAt, string departureCountryCode, string arrivalCountryCode, string departureAirportCode, string arrivalAirportCode, string carrierDesignator)
            : base(departureAt, arrivalAt, departureCountryCode, arrivalCountryCode, carrierDesignator)
        {
            Ensure.That(departureAirportCode, "departureAirportCode").IsNotNullOrEmpty();
            Ensure.That(arrivalAirportCode, "arrivalAirportCode").IsNotNullOrEmpty();

            DepartureAirportCode = departureAirportCode;
            ArrivalAirportCode = arrivalAirportCode;
        }
    }
}