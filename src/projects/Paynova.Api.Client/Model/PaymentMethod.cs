using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class PaymentMethod
    {
        public int Id { get; private set; }

        private PaymentMethod(int id)
        {
            Ensure.That(id, "id").IsGte(0);

            Id = id;
        }

        /// <summary>
        /// Used to create <see cref="PaymentMethod"/> instances with custom values.
        /// As far ass possible, use preconfigured static properties to create instances.
        /// E.g. <see cref="Visa"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PaymentMethod Custom(int id)
        {
            return new PaymentMethod(id);
        }

        public static PaymentMethod All
        {
            get { return new PaymentMethod(0); }
        }

        public static PaymentMethod AllCardTypes
        {
            get { return new PaymentMethod(99); }
        }

        public static PaymentMethod Visa
        {
            get { return new PaymentMethod(1); }
        }

        public static PaymentMethod MasterCard
        {
            get { return new PaymentMethod(2); }
        }

        public static PaymentMethod AmericanExpress
        {
            get { return new PaymentMethod(3); }
        }

        public static PaymentMethod DinersClub
        {
            get { return new PaymentMethod(4); }
        }

        public static PaymentMethod MaestroInternational
        {
            get { return new PaymentMethod(12); }
        }

        public static PaymentMethod NordeaSweden
        {
            get { return new PaymentMethod(101); }
        }

        public static PaymentMethod NordeaFinland
        {
            get { return new PaymentMethod(113); }
        }

        public static PaymentMethod Swedbank
        {
            get { return new PaymentMethod(102); }
        }

        public static PaymentMethod Handelsbanken
        {
            get { return new PaymentMethod(103); }
        }

        public static PaymentMethod SEB
        {
            get { return new PaymentMethod(104); }
        }

        public static PaymentMethod Giropay
        {
            get { return new PaymentMethod(105); }
        }

        public static PaymentMethod iDEAL
        {
            get { return new PaymentMethod(110); }
        }

        public static PaymentMethod ELV
        {
            get { return new PaymentMethod(111); }
        }

        public static PaymentMethod Aktia
        {
            get { return new PaymentMethod(114); }
        }

        public static PaymentMethod DanskeBankFinland
        {
            get { return new PaymentMethod(115); }
        }

        public static PaymentMethod DanskeBankDenmark
        {
            get { return new PaymentMethod(121); }
        }

        public static PaymentMethod ChinaPay
        {
            get { return new PaymentMethod(116); }
        }

        public static PaymentMethod ChinaPayDomestic
        {
            get { return new PaymentMethod(119); }
        }

        public static PaymentMethod Pohjola
        {
            get { return new PaymentMethod(117); }
        }

        public static PaymentMethod Überweisung
        {
            get { return new PaymentMethod(118); }
        }

        public static PaymentMethod Sofortbanking
        {
            get { return new PaymentMethod(123); }
        }

        public static PaymentMethod Skrill
        {
            get { return new PaymentMethod(302); }
        }

        public static PaymentMethod PayPal
        {
            get { return new PaymentMethod(304); }
        }
    }
}