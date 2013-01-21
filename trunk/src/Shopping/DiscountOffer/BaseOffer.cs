namespace Digital7.Shopping.DiscountOffer
{
    using System.Collections.Generic;

    public abstract class BaseOffer
    {
        public abstract double Calculate(List<InventoryItem> inventoryItems);
    }
}