namespace Digital7.Shopping.DiscountOffer
{
    using System.Collections.Generic;
    using System.Linq;

    public class PriceForMany : BaseOffer
    {
        private readonly int skuCount;

        private readonly double skuTotal;

        public PriceForMany(int skuCount, double skuTotal)
        {
            this.skuCount = skuCount;
            this.skuTotal = skuTotal;
        }

        public override double Calculate(List<InventoryItem> inventoryItems)
        {
            // Discount does not get applied
            if (inventoryItems.Count < this.skuCount)
            {
                return inventoryItems.Sum(n => n.Price);
            }

            // See how many times the discount can be applied
            int discountItems = inventoryItems.Count / this.skuCount;

            // See how many are left after the discount
            int nonDiscountItems = inventoryItems.Count % this.skuCount;

            double total = discountItems * this.skuTotal;
            total = total + inventoryItems.Take(nonDiscountItems).Sum(n => n.Price);

            return total;
        }
    }
}