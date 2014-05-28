using System.Collections.Generic;

namespace Paynova.Api.Client.Security
{
    public class Payment
    {
        protected IDictionary<string, string> State { get; set; }

        public int Number { get; private set; }
        public string TransactionId { get; private set; }
        public string Status { get; private set; }
        public string Amount { get; private set; }

        public Payment(int number, IDictionary<string, string> postedValues)
        {
            Number = number;
            State = postedValues;

            InitializeFields();
        }

        protected void InitializeFields()
        {
            TransactionId = GetString("TRANSACTION_ID");
            Status = GetString("STATUS");
            Amount = GetString("AMOUNT");
        }

        protected virtual string GetString(string field)
        {
            field = GenerateFullFieldName(field);

            return State.ContainsKey(field) ? State[field] : null;
        }

        protected virtual string GenerateFullFieldName(string suffix)
        {
            return string.Format("PAYMENT_{0}_{1}", Number, suffix);
        }
    }
}