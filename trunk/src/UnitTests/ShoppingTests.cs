namespace Digital7.UnitTests
{
    using System.Collections.Generic;

    using Digital7.Shopping;
    using Digital7.Shopping.DiscountOffer;

    using FluentAssertions;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class ShoppingTests
    {
        private RuleFactory ruleFactory;

        [SetUp]
        public void SetUp()
        {
            Rule ruleA = new Rule(new InventoryItem("A", 50), new PriceForMany(3, 130));
            Rule ruleB = new Rule(new InventoryItem("B", 30), new PriceForMany(2, 45));
            Rule ruleC = new Rule(new InventoryItem("C", 20), null);
            Rule ruleD = new Rule(new InventoryItem("D", 15), null);

            // Set up stub for ruleFactory
            IRuleCreator ruleCreator = Substitute.For<IRuleCreator>();
            ruleCreator.Load().Returns(new List<Rule> { ruleA, ruleB, ruleC, ruleD });
            this.ruleFactory = new RuleFactory(ruleCreator);
        }

        [Test]
        public void Total_DoNotScanAnything_Returns0()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan(string.Empty);

            checkout.Total().Should().Be(0);
        }

        [Test]
        public void Total_Scan1ItemNoDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan("A");

            checkout.Total().Should().Be(50);
        }

        [Test]
        public void Total_ScanManyItemNoDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("B");
            checkout.Scan("A");

            checkout.Total().Should().Be(115);
        }

        [Test]
        public void Total_Scan3ItemsWithDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            checkout.Total().Should().Be(130);
        }

        [Test]
        public void Total_ScanManyAllItemsWithDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            checkout.Total().Should().Be(175);
        }

        [Test]
        public void Total_ScanManyAllItemsWithOutDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.ruleFactory.Load());
            checkout.Scan("C");
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("D");

            checkout.Total().Should().Be(70);
        }
    }
}