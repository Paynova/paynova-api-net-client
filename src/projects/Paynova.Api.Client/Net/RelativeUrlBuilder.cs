using System;
using System.Collections.Generic;
using System.Linq;

namespace Paynova.Api.Client.Net
{
    public class RelativeUrlBuilder
    {
        protected IRuntime Runtime { get; private set; }
        protected Dictionary<string, string> State { get; private set; }

        public RelativeUrlBuilder(IRuntime runtime)
        {
            Runtime = runtime;
            State = new Dictionary<string, string>();
        }

        public virtual RelativeUrlBuilder Register(Func<Keys, string> keySelector, Guid value)
        {
            return Register(keySelector(Keys.Instance), value.ToString("n"));
        }

        public virtual RelativeUrlBuilder Register(Func<Keys, string> keySelector, decimal value)
        {
            return Register(keySelector(Keys.Instance), value.ToString(Runtime.NumberFormatProvider));
        }

        public virtual RelativeUrlBuilder Register(Func<Keys, string> keySelector, string value)
        {
            Register(keySelector(Keys.Instance), value);

            return this;
        }

        protected virtual RelativeUrlBuilder Register(string key, string value)
        {
            State.Add(key, value);

            return this;
        }

        public virtual string ApplyTo(string relativeUrlFormat)
        {
            return State.Aggregate(relativeUrlFormat, (current, kv) => current.Replace(string.Concat("{", kv.Key, "}"), kv.Value));
        }

        public class Keys
        {
            public static Keys Instance { get; private set; }
            public string OrderId { get { return "orderId"; } }
            public string OrderNumber { get { return "orderNumber"; } }
            public string GovernmentId { get { return "governmentId"; } }
            public string ProfileId { get { return "profileId"; } }
            public string CardId { get { return "cardId"; } }
            public string TransactionId { get { return "transactionId"; } }
            public string CountryCode { get { return "countryCode"; } }
            public string CurrencyCode { get { return "currencyCode"; } }
            public string TotalAmount { get { return "totalAmount"; } }

            private Keys()
            { }

            static Keys()
            {
                Instance = new Keys();
            }
        }
    }
}