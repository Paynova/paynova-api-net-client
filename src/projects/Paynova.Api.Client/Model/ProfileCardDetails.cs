using System;

namespace Paynova.Api.Client.Model
{
    public class ProfileCardDetails
    {
        /// <summary>
        /// Paynova's unique identifier for the profile card stored within a customer profile.
        /// This id should be used for subsequent requests to Paynova involving profile card payments.
        /// </summary>
        public Guid CardId { get; set; }

        /// <summary>
        /// The four-digit expiration year of the card.
        /// </summary>
        public int ExpirationYear { get; set; }

        /// <summary>
        /// The two-digit expiration month of the card.
        /// </summary>
        public int ExpirationMonth { get; set; }

        /// <summary>
        /// The first six digits of the card number (BIN/IIN).
        /// </summary>
        public string FirstSix { get; set; }

        /// <summary>
        /// The last four digits of the card number.
        /// </summary>
        public string LastFour { get; set; }
    }
}