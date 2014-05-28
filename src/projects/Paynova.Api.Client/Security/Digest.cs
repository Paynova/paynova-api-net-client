using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Paynova.Api.Client.Extensions;

namespace Paynova.Api.Client.Security
{
    public abstract class Digest
    {
        public virtual string Calculate(NameValueCollection data, string secretKey)
        {
            var keyValues = data.ToDictionary();
            if (keyValues == null || !keyValues.Any())
                return null;

            var rawData = GetRawString(secretKey, keyValues);
            return string.IsNullOrEmpty(rawData)
                ? null
                : GetSha1String(rawData);
        }

        protected virtual string GetSha1String(string rawData)
        {
            var bytes = GetSha1Bytes(rawData);
            if (bytes == null || !bytes.Any())
                return null;

            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToUpper();
        }

        protected virtual byte[] GetSha1Bytes(string rawData)
        {
            var bytes = Encoding.UTF8.GetBytes(rawData);

            using (var crypt = SHA1.Create())
                return crypt.ComputeHash(bytes);
        }

        protected abstract string GetRawString(string secretKey, Dictionary<string, string> keyValues);

        protected virtual void AppendIfValueExists(string value, StringBuilder rawData)
        {
            if (value != null)
                rawData.Append(value).Append(";");
        }

        protected virtual void AppendIfKeyHasValue(string key, IDictionary<string, string> keyValues, StringBuilder rawData)
        {
            if (!keyValues.ContainsKey(key))
                return;

            var value = keyValues[key];

            AppendIfValueExists(value, rawData);
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