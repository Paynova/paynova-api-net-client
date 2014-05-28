using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class ProfileCard
    {
        /// <summary>
        /// Paynova's unique id for the card stored in the customer profile.
        /// </summary>
        public Guid CardId { get; private set; }

        /// <summary>
        /// Depending on the payment channel and your acquiring agreement, the card CVC
        /// (three or four digit security code) may be required. Paynova will inform you if
        /// you are required to send this information.
        /// </summary>
        public string Cvc { get; set; }

        /// <summary>
        /// Creates an new instance.
        /// </summary>
        /// <param name="cardId">
        /// Paynova's unique id for the card stored in the customer profile.
        /// </param>
        public ProfileCard(Guid cardId)
        {
            Ensure.That(cardId, "cardId").IsNotEmpty();

            CardId = cardId;
        }

        /// <summary>
        /// Creates an new instance.
        /// </summary>
        /// <param name="cardId">
        /// Paynova's unique id for the card stored in the customer profile.
        /// </param>
        /// <param name="cvc">
        /// Depending on the payment channel and your acquiring agreement, the card CVC
        /// (three or four digit security code) may be required. Paynova will inform you if
        /// you are required to send this information.
        /// </param>
        public ProfileCard(Guid cardId, string cvc)
            : this(cardId)
        {
            Ensure.That(cvc, "cvc").IsNotNullOrEmpty();

            Cvc = cvc;
        }
    }
}