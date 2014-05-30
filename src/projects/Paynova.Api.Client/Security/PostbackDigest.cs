using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paynova.Api.Client.Security
{
    public class PostbackDigest : KeyValueBasedDigest
    {
        public PostbackDigest(string secretKey) : base(secretKey)
        {
        }

        protected override string BuildRawStringForCalculation(string secretKey, IDictionary<string, string> keyValues)
        {
            var rawData = new StringBuilder();

            AppendIfKeyHasValue("ORDER_ID", keyValues, rawData);
            AppendIfKeyHasValue("SESSION_ID", keyValues, rawData);
            AppendIfKeyHasValue("ORDER_NUMBER", keyValues, rawData);
            AppendIfKeyHasValue("SESSION_STATUS", keyValues, rawData);
            AppendIfKeyHasValue("CURRENCY_CODE", keyValues, rawData);

            foreach (var payment in GetPayments(keyValues))
            {
                AppendIfValueExists(payment.Status, rawData);
                AppendIfValueExists(payment.TransactionId, rawData);
                AppendIfValueExists(payment.Amount, rawData);
            }

            AppendIfValueExists(secretKey, rawData);

            return rawData.ToString();
        }

        protected virtual IEnumerable<Payment> GetPayments(IDictionary<string, string> keyValues)
        {
            return keyValues
                .Select(kv => new
                {
                    Key = kv.Key,
                    Value = kv.Value,
                    PaymentNumber = GetPaymentNumber(kv.Key)
                })
                .Where(i => i.PaymentNumber.HasValue)
                .GroupBy(i => i.PaymentNumber.Value, i => i)
                .Select(i => new Payment(i.Key, i.ToDictionary(kv => kv.Key, kv => kv.Value)));
        }

        protected virtual bool IsPayment(string key)
        {
            return GetPaymentNumber(key).HasValue;
        }

        protected virtual int? GetPaymentNumber(string key)
        {
            if (!key.StartsWith("PAYMENT_"))
                return null;

            var parts = key.Split(new[] { '_' });
            if (parts.Length < 3)
                return null;

            int num;

            if (int.TryParse(parts[1], out num))
                return num;

            return null;
        }
    }
}