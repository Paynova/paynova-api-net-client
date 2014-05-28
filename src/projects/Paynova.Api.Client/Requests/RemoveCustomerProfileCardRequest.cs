using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Requests
{
    public class RemoveCustomerProfileCardRequest : Request
    {
        /// <summary>
        /// Gets your identifier for the customer.
        /// </summary>
        public string ProfileId { get; private set; }

        /// <summary>
        /// Gets Paynova's identifier for the card associated with the customer profile.
        /// </summary>
        public Guid CardId { get; private set; }

        public RemoveCustomerProfileCardRequest(string profileId, Guid cardId)
        {
            Ensure.That(profileId, "profileId").IsNotNullOrEmpty();
            Ensure.That(cardId, "cardId").IsNotEmpty();

            ProfileId = profileId;
            CardId = cardId;
        }
    }
}