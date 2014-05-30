using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Resources;

namespace Paynova.Api.Client.Security
{
    public abstract class KeyValueBasedDigest : DigestBase
    {
        protected const string Delimitor = ";";

        protected KeyValueBasedDigest(string secretKey) : base(secretKey)
        {
        }

        public virtual bool Validate(NameValueCollection data)
        {
            return Validate(data.ToDictionary());
        }

        public virtual bool Validate(IDictionary<string, string> data)
        {
            string calculatedDigest;

            return TryValidate(data, out calculatedDigest);
        }

        public virtual bool TryValidate(NameValueCollection data, out string calculatedDigest)
        {
            return TryValidate(data.ToDictionary(), out calculatedDigest);
        }

        public virtual bool TryValidate(IDictionary<string, string> data, out string calculatedDigest)
        {
            Ensure.That(data, "data").IsNotNull();
            Ensure.That(data.ContainsKey("DIGEST"), "data")
                .WithExtraMessageOf(() => ExceptionMessages.DigestValidation_MissingOriginalDigest)
                .IsTrue();

            var originalDigest = data["DIGEST"];
            calculatedDigest = Calculate(data);

            return originalDigest.Equals(calculatedDigest, StringComparison.Ordinal);
        }

        public virtual string Calculate(NameValueCollection data)
        {
            Ensure.That(data, "data").IsNotNull();

            return Calculate(data.ToDictionary());
        }

        public virtual string Calculate(IDictionary<string, string> data)
        {
            Ensure.That(data, "data").IsNotNull();

            if (!data.Any())
                return null;

            var rawData = BuildRawStringForCalculation(SecretKey, data);
            return string.IsNullOrEmpty(rawData)
                ? null
                : GenerateSha1String(rawData.TrimEnd(Delimitor.ToCharArray()));
        }

        protected abstract string BuildRawStringForCalculation(string secretKey, IDictionary<string, string> keyValues);

        protected virtual void AppendIfValueExists(string value, StringBuilder rawData)
        {
            if (value != null)
                rawData.Append(value).Append(Delimitor);
        }

        protected virtual void AppendIfKeyHasValue(string key, IDictionary<string, string> keyValues, StringBuilder rawData)
        {
            if (!keyValues.ContainsKey(key))
                return;

            var value = keyValues[key];

            AppendIfValueExists(value, rawData);
        }
    }
}