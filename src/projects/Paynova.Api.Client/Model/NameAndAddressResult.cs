namespace Paynova.Api.Client.Model
{
    public class NameAndAddressResult
    {
        /// <summary>
        /// The name of the customer and/or company.
        /// </summary>
        public CompanyOrPersonName Name { get; set; }

        /// <summary>
        /// The address.
        /// </summary>
        public AddressResult Address { get; set; }

        public NameAndAddressResult()
        {
            Name = new CompanyOrPersonName();
            Address = new AddressResult();
        }
    }
}