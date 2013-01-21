namespace Digital7.Shopping
{
    using System.Collections.Generic;

    public class Checkout
    {
        private readonly List<Rule> rules;

        private readonly List<string> scannedInventoryItems = new List<string>();

        public Checkout(List<Rule> rules)
        {
            this.rules = rules;
        }

        public void Scan(string sku)
        {
            if (string.IsNullOrEmpty(sku))
            {
                return;
            }

            this.scannedInventoryItems.Add(sku);
        }

        public double Total()
        {
            if (this.scannedInventoryItems.Count == 0)
            {
                return 0;
            }

            return -1;
        }
    }
}