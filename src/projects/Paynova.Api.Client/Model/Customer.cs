namespace Paynova.Api.Client.Model
{
    public class Customer
    {
        /// <summary>
        /// Your unique identifier for the customer.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The customer's e-mail address.          
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The name of the customer and/or company.
        /// </summary>
        public CompanyOrPersonName Name { get; set; }

        /// <summary>
        /// The customer's home telephone number including the national prefix.
        /// Example: +468 555 55 55
        /// </summary>
        public string HomeTelephone { get; set; }

        /// <summary>
        /// The customer's work telephone number including the national prefix.
        /// Example: +468 555 55 55
        /// </summary>
        public string WorkTelephone { get; set; }

        /// <summary>
        /// The customer's mobile telephone number including the national prefix.
        /// Example: +46722 55 55 55
        /// </summary>
        public string MobileTelephone { get; set; }

        public Customer()
        {
            Name = new CompanyOrPersonName();
        }
    }
}