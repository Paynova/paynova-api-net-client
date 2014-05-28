using System.Collections.Generic;
using System.Text;

namespace Paynova.Api.Client.Security
{
    public class PostbackDigest : Digest
    {
        protected override string GetRawString(string secretKey, Dictionary<string, string> keyValues)
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

            return rawData.ToString().TrimEnd(new[] { ';' });
        }
    }
}