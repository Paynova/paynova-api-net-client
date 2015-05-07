namespace Paynova.Api.Client.Model
{
    public class LabelSymbolValue<T>
    {
        public string Label { get; set; }
        public string Symbol { get; set; }
        public T Value { get; set; }
    }
}