using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    /// <summary>
    ///  Currency code entity. Should follow ISO 4217.
    /// <![CDATA[http://docs.paynova.com]]>
    /// </summary>
    public class CurrencyCode
    {
        public string Alpha3 { get; private set; }
        public string Iso { get; private set; }

        private CurrencyCode(string alpha3, string iso)
        {
            Ensure.That(iso, "iso").IsNotNullOrEmpty();
            Ensure.That(iso.Length == 3, "iso.Length").IsTrue();
            Ensure.That(alpha3, "alpha3").IsNotNullOrEmpty();

            Iso = iso;
            Alpha3 = alpha3;
        }

        /// <summary>
        /// Used to create <see cref="CurrencyCode"/> instances with custom values.
        /// As far ass possible, use preconfigured static properties to create instances.
        /// E.g. <see cref="UnitedStatesDollar"/>.
        /// </summary>
        /// <param name="alpha3"></param>
        /// <param name="iso"></param>
        /// <returns></returns>
        public static CurrencyCode Custom(string alpha3, string iso)
        {
            return new CurrencyCode(alpha3, iso);
        }

        public override string ToString()
        {
            return Alpha3;
        }

        public static CurrencyCode UnitedStatesDollar
        {
            get { return new CurrencyCode("USD", "840"); }
        }

        public static CurrencyCode EuropeanEuro
        {
            get { return new CurrencyCode("EUR", "978"); }
        }

        public static CurrencyCode BritishPound
        {
            get { return new CurrencyCode("GBP", "826"); }
        }

        public static CurrencyCode SwedishKrona
        {
            get { return new CurrencyCode("SEK", "752"); }
        }

        public static CurrencyCode NorwegianKrone
        {
            get { return new CurrencyCode("NOK", "578"); }
        }

        public static CurrencyCode DanishKrone
        {
            get { return new CurrencyCode("DKK", "208"); }
        }

        public static CurrencyCode SwissFranc
        {
            get { return new CurrencyCode("CHF", "756"); }
        }

        public static CurrencyCode AustralianDollar
        {
            get { return new CurrencyCode("AUD", "036"); }
        }

        public static CurrencyCode NewZealandDollar
        {
            get { return new CurrencyCode("NZD", "554"); }
        }

        public static CurrencyCode HongKongDollar
        {
            get { return new CurrencyCode("HKD", "344"); }
        }

        public static CurrencyCode SingaporeDollar
        {
            get { return new CurrencyCode("SGD", "702"); }
        }

        public static CurrencyCode CanadianDollar
        {
            get { return new CurrencyCode("CAD", "124"); }
        }

        public static CurrencyCode PolishZłoty
        {
            get { return new CurrencyCode("PLN", "985"); }
        }

        public static CurrencyCode TurkishLira
        {
            get { return new CurrencyCode("TRY", "949"); }
        }

        public static CurrencyCode ChineseYuan
        {
            get { return new CurrencyCode("CNY", "156"); }
        }

        public static CurrencyCode JapaneseYen
        {
            get { return new CurrencyCode("JPY", "392"); }
        }

        public static CurrencyCode IsraeliNewSheqel
        {
            get { return new CurrencyCode("ILS", "376"); }
        }
    }
}