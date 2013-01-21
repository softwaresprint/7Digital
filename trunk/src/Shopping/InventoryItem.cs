namespace Digital7.Shopping
{
    public class InventoryItem
    {
        public InventoryItem(string sku, double price)
        {
            this.Sku = sku;
            this.Price = price;
        }

        public string Sku { get; private set; }

        public double Price { get; private set; }
    }
}