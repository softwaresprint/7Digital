namespace Digital7.Shopping
{
    using Digital7.Shopping.DiscountOffer;

    public class Rule
    {
        public Rule(InventoryItem inventoryItem, BaseOffer offer)
        {
            this.Item = inventoryItem;
            this.Offer = offer;
        }

        public InventoryItem Item { get; private set; }

        public BaseOffer Offer { get; private set; }
    }
}