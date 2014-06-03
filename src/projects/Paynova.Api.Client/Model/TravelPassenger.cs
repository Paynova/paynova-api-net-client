namespace Paynova.Api.Client.Model
{
    public class TravelPassenger
    {
        /// <summary>
        /// The passenger's name.
        /// </summary>
        public CompanyOrPersonName Name { get; set; }

        /// <summary>
        /// The passenger's telephone number.
        /// The telephone number, including the national prefix.
        /// Example: +468 555 55 55
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// The passenger's e-mail address.
        /// </summary>
        public string EmailAddress { get; set; }

        public TravelPassenger()
        {
            Name = new CompanyOrPersonName();
        }
    }
}