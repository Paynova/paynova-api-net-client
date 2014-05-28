namespace Paynova.Api.Client.Model
{
    public class CompanyOrPersonName
    {
        /// <summary>
        /// The company's name, if the purchase is being made on behalf of a company.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The title. Example: "Mr.", "Mrs.".
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The first name (given name).
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The middle names.
        /// </summary>
        public string MiddleNames { get; set; }

        /// <summary>
        /// The last name (surname).
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The name's suffix. Example: "Sr.", "Jr.".
        /// </summary>
        public string Suffix { get; set; }
    }
}