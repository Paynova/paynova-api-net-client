using System.Collections.Generic;
using System.Text;

namespace Paynova.Api.Client.Security
{
    public class CallbackDigest : Digest
    {
        protected override string GetRawString(string secretKey, Dictionary<string, string> keyValues)
        {
            var rawData = new StringBuilder();

            AppendIfKeyHasValue("EVENT_TYPE", keyValues, rawData);
            AppendIfKeyHasValue("EVENT_TIMESTAMP", keyValues, rawData);
            AppendIfKeyHasValue("DELIVERY_TIMESTAMP", keyValues, rawData);
            AppendIfKeyHasValue("MERCHANT_ID", keyValues, rawData);
            AppendIfValueExists(secretKey, rawData);

            return rawData.ToString().TrimEnd(new[] { ';' });
        }
    }
}