namespace Paynova.Api.Client.Model
{
    public class LineItemGroupKeys
    {
        private readonly static LineItemGroupKeys InternalInstance = new LineItemGroupKeys();

        public static LineItemGroupKeys Instance { get { return InternalInstance; } }

        public string Extra { get; private set; }
        public string Discount { get; private set; }
        public string Shipping { get; private set; }
        public string Tax { get; private set; }
        public string Item { get; private set; }

        private LineItemGroupKeys()
        {
            Extra = "_EXTRA_";
            Discount = "_DISCOUNT_";
            Shipping = "_SHIPPING_";
            Tax = "_TAX_";
            Item = "_ITEM_";
        }
    }
}