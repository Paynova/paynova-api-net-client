using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Requests
{
    public class GetAddressesRequest : Request
    {
        /// <summary>
        /// Gets the government id to be looked up.
        /// </summary>
        public string GovernmentId { get; private set; }

        /// <summary>
        /// Gets the country code to which the <see cref="GovernmentId"/>
        /// exists in.
        /// </summary>
        public string CountryCode { get; private set; }

        /// <summary>
        /// Creates request for getting addresses.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="governmentId"></param>
        public GetAddressesRequest(string countryCode, string governmentId)
        {
            Ensure.That(countryCode, "countryCode").IsNotNullOrEmpty();
            Ensure.That(governmentId, "governmentId").IsNotNullOrEmpty();

            CountryCode = countryCode;
            GovernmentId = governmentId;
        }
    }
}