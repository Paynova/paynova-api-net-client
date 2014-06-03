using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class InterfaceOptions
    {
        /// <summary>
        /// The id of the web interface to use.
        /// </summary>
        public int InterfaceId { get; private set; }

        /// <summary>
        /// Indicates whether or not order line-items should be
        /// displayed to the customer. The default is true if
        /// you send line items, false if you do not send line items.
        /// </summary>
        public bool? DisplayLineItems { get; set; }

        /// <summary>
        /// The name of your custom hosted theme at Paynova. Note
        /// that this field only applies to merchants who have setup a
        /// custom theme with us and specifying an invalid value will
        /// result in your payment page not rendering properly.
        /// </summary>
        public string ThemeName { get; set; }

        /// <summary>
        /// The name of the layout to use.
        /// For assistance <see cref="Model.LayoutNames"/>.
        /// Example: Paynova_FullPage_1, Paynova_Mobile_1
        /// </summary>
        public string LayoutName { get; set; }

        /// <summary>
        /// The three-letter language code identifying the language
        /// that the payment interface should be displayed to the
        /// customer in.
        /// </summary>
        public string CustomerLanguageCode { get; private set; }
        
        /// <summary>
        /// The URL on your website to which we should redirect the
        /// customer upon successful payment.
        /// </summary>
        public Uri UrlRedirectSuccess { get; private set; }

        /// <summary>
        /// The URL on your website to which we should redirect the
        /// customer upon the customer cancelling payment or running
        /// out of payment attempts.
        /// </summary>
        public Uri UrlRedirectCancel { get; private set; }

        /// <summary>
        /// The URL on your website to which we should redirect the
        /// customer upon a payment being in either an indeterminable
        /// or pending state. Payment methods which are not "real-time"
        /// (for example, Laschrift/ELV, Überweisung) use this status.
        /// </summary>
        public Uri UrlRedirectPending { get; private set; }

        /// <summary>
        /// A URL to your system which we can send Event Hook Notifications
        /// (EHNs) to. If this parameter is provided, it will be used
        /// instead of any statically configured EHN URLs.
        /// </summary>
        public Uri UrlCallback { get; set; }

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        /// <param name="interfaceId">
        /// The id of the web interface to use.
        /// Helpers exists, e.g: <see cref="Model.InterfaceId.Aero"/>.
        /// </param>
        /// <param name="customerLanguageCode">
        /// The three-letter language code identifying the language
        /// that the payment interface should be displayed to the
        /// customer in.
        /// </param>
        /// <param name="urlRedirectSuccess">
        /// The URL on your website to which we should redirect the
        /// customer upon successful payment.
        /// </param>
        /// <param name="urlRedirectCancel">
        /// The URL on your website to which we should redirect the
        /// customer upon the customer cancelling payment or running
        /// out of payment attempts.
        /// </param>
        /// <param name="urlRedirectPending">
        /// The URL on your website to which we should redirect the
        /// customer upon a payment being in either an indeterminable
        /// or pending state. Payment methods which are not "real-time"
        /// (for example, Laschrift/ELV, Überweisung) use this status.
        /// </param>
        public InterfaceOptions(InterfaceId interfaceId, string customerLanguageCode, Uri urlRedirectSuccess, Uri urlRedirectCancel, Uri urlRedirectPending)
        {
            Ensure.That(customerLanguageCode, "customerLanguageCode").IsNotNullOrEmpty();
            Ensure.That(urlRedirectSuccess, "urlRedirectSuccess").IsNotNull();
            Ensure.That(urlRedirectCancel, "urlRedirectCancel").IsNotNull();
            Ensure.That(urlRedirectPending, "urlRedirectPending").IsNotNull();

            InterfaceId = interfaceId;
            CustomerLanguageCode = customerLanguageCode;
            UrlRedirectSuccess = urlRedirectSuccess;
            UrlRedirectCancel = urlRedirectCancel;
            UrlRedirectPending = urlRedirectPending;
        }

        /// <summary>
        /// Sets <see cref="LayoutName"/> and returns the current
        /// see <see cref="InterfaceOptions"/> for fluent config.
        /// Look at <see cref="Model.LayoutNames"/> for some pre-defined
        /// values.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual InterfaceOptions WithLayoutName(Func<LayoutNames, string> selector)
        {
            LayoutName = selector(LayoutNames.Instance);

            return this;
        }
    }
}