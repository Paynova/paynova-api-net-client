using Paynova.Api.Client.Model;

namespace Paynova.Api.Client.Responses
{
    public class GetAddressesResponse : Response
    {
        public string GovernmentId { get; set; }
        public string CountryCode { get; set; }
        public NameAndAddress[] Addresses { get; set; }
    }
}