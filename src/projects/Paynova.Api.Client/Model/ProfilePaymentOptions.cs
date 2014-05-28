using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class ProfilePaymentOptions
    {
        /// <summary>
        /// Your identifier for the customer profile.
        /// </summary>
        public string ProfileId { get; private set; }

        /// <summary>
        /// The profile card use in the payment. If the payment is to be
        /// performed on a stored profile card, this parameter is required.
        /// </summary>
        public ProfileCard ProfileCard { get; set; }

        /// <summary>
        /// If you would like the customer to choose whether or not to save
        /// their card in your customer profile on Paynova's page, then set this option to true.
        /// </summary>
        /// <remarks>
        /// If you provide a profile id in profilePaymentOptions and this value is false, the
        /// card will be saved and the customer will not be presented with an option on Paynova's
        /// payment page.
        ///
        /// If you provide a profile id in profilePaymentOptions and this value is not present
        /// (null), the card will be saved.
        ///
        /// If you provide a profile id in profilePaymentOptions and this value is true, the
        /// customer will be presented with the option to choose whether or not they want their
        /// card saved on Paynova's payment page. If the customer opts to save their card, you
        /// will receive a profile card identifier after the payment has been completed
        /// (assuming the customer pays with a card), otherwise the card will not be saved to
        /// the customer's profile and you will not receive an identifier.
        /// </remarks>
        public bool? DisplaySaveProfileCardOption { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="ProfilePaymentOptions"/> with required data.
        /// </summary>
        /// <param name="profileId">Your identifier for the customer profile.</param>
        public ProfilePaymentOptions(string profileId)
        {
            Ensure.That(profileId, "profileId").IsNotNullOrEmpty();

            ProfileId = profileId;
        }

        /// <summary>
        /// Creates an instance of <see cref="ProfilePaymentOptions"/> with required data.
        /// </summary>
        /// <param name="profileId">Your identifier for the customer profile.</param>
        /// <param name="cardId">Paynova's unique id for the card stored in the customer profile.</param>
        public ProfilePaymentOptions(string profileId, Guid cardId)
            : this(profileId)
        {
            ProfileCard = new ProfileCard(cardId);
        }

        /// <summary>
        /// Creates an instance of <see cref="ProfilePaymentOptions"/> with required data.
        /// </summary>
        /// <param name="profileId">Your identifier for the customer profile.</param>
        /// <param name="cardId">Paynova's unique id for the card stored in the customer profile.</param>
        /// <param name="cvc">
        /// Depending on the payment channel and your acquiring agreement, the card CVC
        /// (three or four digit security code) may be required. Paynova will inform you if
        /// you are required to send this information.
        /// </param>
        public ProfilePaymentOptions(string profileId, Guid cardId, string cvc)
            : this(profileId)
        {
            ProfileCard = new ProfileCard(cardId, cvc);
        }
    }
}