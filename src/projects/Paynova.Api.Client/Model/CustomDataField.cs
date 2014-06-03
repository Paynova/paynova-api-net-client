using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Model
{
    public class CustomDataField
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public CustomDataField(string key, string value)
        {
            Ensure.That(key, "key").IsNotNullOrEmpty();
            Ensure.That(value, "value").IsNotNullOrEmpty();

            Key = key;
            Value = value;
        }
    }
}