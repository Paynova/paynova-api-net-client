namespace Paynova.Api.Client.Model
{
    public class Address
    {
        /// <summary>
        /// Gets the type of address.
        /// </summary>
        /// <remarks>
        /// Only applicable when returned by Paynova.
        /// </remarks>
        public string Type { get; set; }

        /// <summary>
        /// The street address, line 1.
        /// </summary>
        public string Street1 { get; set; }

        /// <summary>
        /// The street address, line 2.
        /// </summary>
        public string Street2 { get; set; }

        /// <summary>
        /// The street address, line 3.
        /// </summary>
        public string Street3 { get; set; }

        /// <summary>
        /// The street address, line 4.
        /// </summary>
        public string Street4 { get; set; }

        /// <summary>
        /// The city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The postal/zip code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// The region code/state code.
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// The country code. This may be the two-letter (alpha-2),
        /// three-letter (alpha-3) code or the ISO country number as
        /// per ISO 3166-1.
        /// Example: SE, SWE, 752
        /// </summary>
        public string CountryCode { get; set; }
    }
}