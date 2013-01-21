namespace Digital7.UnitTests
{
    using System.Collections.Generic;

    using Digital7.Shopping;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class ShoppingTests
    {
        private List<Rule> rules;

        [SetUp]
        public void SetUp()
        {
            this.rules = new List<Rule>();
        }

        [Test]
        public void Total_DoNotScanAnything_Returns0()
        {
            Checkout checkout = new Checkout(this.rules);
            checkout.Scan(string.Empty);

            checkout.Total().Should().Be(0);
        }

        [Test]
        public void Total_Scan1ItemNoDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.rules);
            checkout.Scan("A");

            checkout.Total().Should().Be(50);
        }

        [Test]
        public void Total_ScanManyItemNoDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.rules);
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("B");
            checkout.Scan("A");

            checkout.Total().Should().Be(115);
        }

        [Test]
        public void Total_Scan3ItemsWithDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.rules);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            checkout.Total().Should().Be(130);
        }

        [Test]
        public void Total_ScanManyAllItemsWithDiscount_ReturnsCorrectPrice()
        {
            Checkout checkout = new Checkout(this.rules);
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
            Checkout checkout = new Checkout(this.rules);
            checkout.Scan("C");
            checkout.Scan("C");
            checkout.Scan("D");
            checkout.Scan("D");

            checkout.Total().Should().Be(70);
        }
    }
}