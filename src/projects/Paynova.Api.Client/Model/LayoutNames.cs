namespace Paynova.Api.Client.Model
{
    public class LayoutNames
    {
        private static readonly LayoutNames InternalInstance = new LayoutNames();

        public static LayoutNames Instance{get { return InternalInstance; }}

        /// <summary>
        /// Layout designed for desktop browsers.
        /// </summary>
        public string Desktop { get; private set; }

        /// <summary>
        /// Layout designed for mobile browsers. Only card payments are supported.
        /// </summary>
        public string Mobile { get; private set; }

        public LayoutNames()
        {
            Desktop = "Paynova_FullPage_1";
            Mobile = "Paynova_Mobile_1";
        }
    }
}