using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Requests
{
    public class RemoveCustomerProfileRequest : Request
    {
        /// <summary>
        /// Gets your identifier for the customer.
        /// </summary>
        public string ProfileId { get; private set; }

        public RemoveCustomerProfileRequest(string profileId)
        {
            Ensure.That(profileId, "profileId").IsNotNullOrEmpty();

            ProfileId = profileId;
        }
    }
}