using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class TravelData
    {
        /// <summary>
        /// The booking reference number for the booking.
        /// </summary>
        public string BookingReference { get; private set; }

        /// <summary>
        /// The travel segments included in this booking.
        /// </summary>
        public TravelSegment[] TravelSegments { get; private set; }

        /// <summary>
        /// Constructs TravelData.
        /// </summary>
        /// <param name="bookingReference">The booking reference number for the booking</param>
        /// <param name="travelSegments">
        /// Required: The travel segments included in this booking.
        /// <see cref="AirTravelSegment"/> and <see cref="RailTravelSegment"/>.
        /// </param>
        public TravelData(string bookingReference, params TravelSegment[] travelSegments)
        {
            Ensure.That(bookingReference, "bookingReference").IsNotNullOrEmpty();
            Ensure.That(travelSegments, "travelSegments").HasItems();

            BookingReference = bookingReference;
            TravelSegments = travelSegments;
        }
    }
}