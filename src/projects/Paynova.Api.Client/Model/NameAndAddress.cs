namespace Paynova.Api.Client.Model
{
    public class NameAndAddress
    {
        /// <summary>
        /// The name of the customer and/or company.
        /// </summary>
        public CompanyOrPersonName Name { get; set; }

        /// <summary>
        /// The address.
        /// </summary>
        public Address Address { get; set; }

        public NameAndAddress()
        {
            Name = new CompanyOrPersonName();
            Address = new Address();
        }
    }
}