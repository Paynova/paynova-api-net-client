using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public struct InterfaceId : IEquatable<int>, IComparable<int>
    {
        private readonly int _value;

        public InterfaceId(int value)
        {
            Ensure.That(value, "value").IsGte(0);

            _value = value;
        }

        /// <summary>
        /// Used to create <see cref="InterfaceId"/> instances with custom values.
        /// As far ass possible, use preconfigured static properties to create instances.
        /// E.g. <see cref="Aero"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static InterfaceId Custom(int value)
        {
            return new InterfaceId(value);
        }

        public static implicit operator int(InterfaceId item)
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
        /// Hosted Areo interface.
        /// <![CDATA[http://docs.paynova.com]]>
        /// </summary>
        /// <returns></returns>
        public static InterfaceId Aero
        {
            get { return new InterfaceId(5); }
        }
    }
}