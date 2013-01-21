﻿namespace Digital7.Shopping
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

            // Sum up all the null offers
            double total = this.JoinedRuleItems().Where(n => n.Offer == null).Sum(n => n.Item.Price);

            foreach (var rule in this.rules.Where(n => n.Offer != null))
            {
                total = total + rule.Offer.Calculate(this.JoinedRuleItems().Select(n => n.Item).Where(n => n.Sku.Equals(rule.Item.Sku)).ToList());
            }

            return total;
        }

        private IEnumerable<Rule> JoinedRuleItems()
        {
            return from n in this.scannedInventoryItems
                   join o in this.rules on n equals o.Item.Sku
                   select o;
        }
    }
}