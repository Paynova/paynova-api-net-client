using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Requests
{
    public class GetCustomerProfileRequest : Request
    {
        /// <summary>
        /// Gets your unique identifier for the customer profile stored at Paynova.
        /// </summary>
        public string ProfileId { get; private set; }

        /// <summary>
        /// Creates request for getting a customer profile.
        /// </summary>
        /// <param name="profileId">
        /// Your unique identifier for the customer profile stored at Paynova.
        /// </param>
        public GetCustomerProfileRequest(string profileId)
        {
            Ensure.That(profileId, "profileId").IsNotNullOrEmpty();

            ProfileId = profileId;
        }
    }
}