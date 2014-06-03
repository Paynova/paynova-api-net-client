using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Responses
{
    public class GetCustomerProfileResponse : Response
    {
        /// <summary>
        /// Your unique identifier for the customer's profile.
        /// </summary>
        public string ProfileId { get; set; }

        /// <summary>
        /// Contains details about any profile cards stored within the customer profile.
        /// </summary>
        public ProfileCardDetails[] ProfileCards { get; set; }
    }
}