namespace Digital7.Shopping
{
    using System.Collections.Generic;
    using System.Linq;

    public class Checkout
    {
        private readonly List<Rule> rules;

        private readonly List<string> scannedInventoryItems = new List<string>();

        public Checkout(List<Rule> rules)
        {
            if (rules == null)
            {
                this.rules = new List<Rule>();
            }

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

            if (this.rules.Count == 0)
            {
                return 0;
            }

            var total = this.OfferItemsTotal() + this.OfferlessItemsTotal();

            return total;
        }

        private double OfferItemsTotal()
        {
            double total = 0;

            foreach (var rule in this.rules.Where(n => n.Offer != null))
            {
                Rule closureRule = rule;
                var items = from n in this.JoinedRuleItems()
                            where n.Offer != null && n.Item.Sku.Equals(closureRule.Item.Sku)
                            select n.Item;

                total = total + rule.Offer.Calculate(items.ToList());
            }

            return total;
        }

        private double OfferlessItemsTotal()
        {
            // Sum up all the null offers
            return this.JoinedRuleItems().Where(n => n.Offer == null).Sum(n => n.Item.Price);
        }

        private IEnumerable<Rule> JoinedRuleItems()
        {
            return from n in this.scannedInventoryItems
                   join o in this.rules on n equals o.Item.Sku
                   select o;
        }
    }
}