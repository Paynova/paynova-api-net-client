using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public struct PaymentChannelId : IEquatable<int>, IComparable<int>
    {
        private readonly int _value;

        public PaymentChannelId(int value)
        {
            Ensure.That(value, "value").IsGte(0);

            _value = value;
        }

        /// <summary>
        /// Used to create <see cref="PaymentChannelId"/> instances with custom values.
        /// As far ass possible, use preconfigured static properties to create instances.
        /// E.g. <see cref="Web"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PaymentChannelId Custom(int value)
        {
            return new PaymentChannelId(value);
        }

        public static implicit operator int(PaymentChannelId item)
        {
            return item._value;
        }

        public bool Equals(int other)
        {
            return _value.Equals(other);
        }

        public int CompareTo(int other)
        {
            return _value.CompareTo(other);
        }

        /// <summary>
        /// Used for web payments.
        /// </summary>
        public static PaymentChannelId Web
        {
            get { return new PaymentChannelId(1); }
        }

        /// <summary>
        /// Used for payments accepted over the telephone or via the mail.
        /// </summary>
        public static PaymentChannelId Mail
        {
            get { return new PaymentChannelId(2); }
        }

        /// <summary>
        /// Used for payments accepted over the telephone or via the mail.
        /// </summary>
        public static PaymentChannelId Telephone
        {
            get { return new PaymentChannelId(2); }
        }

        /// <summary>
        /// Used for non-customer initiated payments for subscriptions.
        /// </summary>
        /// <returns></returns>
        public static PaymentChannelId Recurring
        {
            get { return new PaymentChannelId(7); }
        }
    }
}