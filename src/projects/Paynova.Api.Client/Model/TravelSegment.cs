using System;
using System.Collections.Generic;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Resources;

namespace Paynova.Api.Client.Model
{
    /// <summary>
    /// Represents a segment of a travel <see cref="TravelData"/>.
    /// Use specific implementations <see cref="AirTravelSegment"/>
    /// and <see cref="RailTravelSegment"/>.
    /// </summary>
    public abstract class TravelSegment
    {
        protected List<TravelTicket> InternalTickets { get; private set; }

        /// <summary>
        /// The means of travel.
        /// </summary>
        public abstract string SegmentType { get; }

        /// <summary>
        /// The scheduled departure date for this segment,
        /// in the format YYYY-MM-DD.
        /// Example: 2013-11-23
        /// </summary>
        public string DepartureDate { get; private set; }

        /// <summary>
        /// The scheduled departure time for this segment,
        /// in the format HH:MM, using the 24-hour time format.
        /// Example: 19:55
        /// </summary>
        public string DepartureTime { get; private set; }

        /// <summary>
        /// The country in which the departure station/airport for this segment is in.
        /// This may be the two-letter (alpha-2), three-letter (alpha-3) code
        /// or the ISO country number as per ISO 3166-1.
        /// Example: SE, SWE, 752
        /// </summary>
        public string DepartureCountryCode { get; private set; }

        /// <summary>
        /// The scheduled arrival date for this segment, in the format YYYY-MM-DD.
        /// Example: 2013-11-23
        /// </summary>
        public string ArrivalDate { get; private set; }

        /// <summary>
        /// The scheduled arrival time for this segment,
        /// in the format HH:MM, using the 24-hour time format.
        /// Example: 19:55
        /// </summary>
        public string ArrivalTime { get; private set; }

        /// <summary>
        /// The country in which the destination station/airport for this segment is in.
        /// This may be the two-letter (alpha-2), three-letter (alpha-3) code
        /// or the ISO country number as per ISO 3166-1.
        /// Example: SE, SWE, 752
        /// </summary>
        public string ArrivalCountryCode { get; private set; }

        /// <summary>
        /// The IATA or ICAO airline code, or the UIC railway code, of the carrier for this segment, in the format "authority:code".
        /// AIR Segments: If you were sending the code for Scandinavian Airlines (SAS), you would send:
        /// For IATA: "IATA:SK"
        /// For ICAO: "ICAO:SAS"
        ///
        /// RAIL Segments: If you were sending the code for Swedish Railway (SJ), you would send:
        /// For UIC you would send: "UIC:1174"
        /// </summary>
        public string CarrierDesignator { get; private set; }

        /// <summary>
        /// The tickets which were issued for this segment of travel.
        /// </summary>
        public TravelTicket[] Tickets
        {
            get { return InternalTickets.ToArray(); }
        }

        protected TravelSegment(DateTime departureAt, DateTime arrivalAt, string departureCountryCode, string arrivalCountryCode, string carrierDesignator)
        {
            Ensure.That(departureAt, "departureAt").IsGt(Runtime.Instance.DateTimeProvider.Now);
            Ensure.That(arrivalAt, "arrivalAt").WithExtraMessageOf(() => ExceptionMessages.TravelSegment_ArivalMustBeAfterDeparture).IsGt(departureAt);
            Ensure.That(departureCountryCode, "departureCountryCode").IsNotNullOrEmpty();
            Ensure.That(arrivalCountryCode, "arrivalCountryCode").IsNotNullOrEmpty();
            Ensure.That(carrierDesignator, "carrierDesignator").IsNotNullOrEmpty();

            DepartureDate = departureAt.ToString("yyyy-MM-dd");
            DepartureTime = departureAt.ToString("HH:mm");

            ArrivalDate = arrivalAt.ToString("yyyy-MM-dd");
            ArrivalTime = arrivalAt.ToString("HH:mm");

            DepartureCountryCode = departureCountryCode;
            ArrivalCountryCode = arrivalCountryCode;
            CarrierDesignator = carrierDesignator;

            InternalTickets = new List<TravelTicket>();
        }
    }

    public abstract class TravelSegment<T> : TravelSegment where T : TravelSegment<T>
    {
        private T Self
        {
            get { return this as T; }
        }

        protected TravelSegment(DateTime departureAt, DateTime arrivalAt, string departureCountryCode, string arrivalCountryCode, string carrierDesignator)
            : base(departureAt, arrivalAt, departureCountryCode, arrivalCountryCode, carrierDesignator)
        {
        }

        public virtual T AddTicket(TravelTicket item)
        {
            Ensure.That(item, "item").IsNotNull();

            InternalTickets.Add(item);

            return Self;
        }

        public virtual T ClearTickets()
        {
            InternalTickets.Clear();

            return Self;
        }

        public virtual T WithTickets(params TravelTicket[] items)
        {
            Ensure.That(items, "items").HasItems();

            InternalTickets.Clear();
            InternalTickets.AddRange(items);

            return Self;
        }

        public virtual T WithTickets(IEnumerable<TravelTicket> items)
        {
            Ensure.That(items, "items").IsNotNull();

            InternalTickets.Clear();
            InternalTickets.AddRange(items);

            return Self;
        }
    }
}