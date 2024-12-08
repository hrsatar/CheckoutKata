namespace CheckoutKata.Tests
{
    public class CheckoutTests
    {
        private List<PricingRule> Rules;

        public CheckoutTests()
        {
            Rules = new List<PricingRule> {
            new PricingRule {SKU="A",SinglePrice=50,SpecialOfferPrice=130,SpecialOfferQuantity=3 },
            new PricingRule {SKU="B",SinglePrice=30,SpecialOfferPrice=45,SpecialOfferQuantity=2 },
            new PricingRule {SKU="C",SinglePrice=20 },
            new PricingRule {SKU="D",SinglePrice=15 }
            };
        }

        [Fact]
        public void ReturnZeroTotalPriceWhenCheckoutIsEmpty()
        {
            Checkout checkout = new Checkout(Rules);

            Assert.Equal(0, checkout.GetTotalPrice());
        }

        [Fact]
        public void ReturnSingleItemPriceWhenSingleItemScannedAndCheckoutIsEmpty()
        {
            Checkout checkout = new Checkout(Rules);

            checkout.Scan("A");

            Assert.Equal(50, checkout.GetTotalPrice());
        }

        [Fact]
        public void ReturnSumSingleItemPriceWhenTwoSingleItemsScannedAndCheckoutIsEmpty()
        {
            Checkout checkout = new Checkout(Rules);

            checkout.Scan("A");
            checkout.Scan("B");

            Assert.Equal(80, checkout.GetTotalPrice());
        }

        [Fact]
        public void ReturnTotalPriceWhenAnItemWithSpecialOfferIsScanned()
        {
            Checkout checkout = new Checkout(Rules);

            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.Equal(130, checkout.GetTotalPrice());
        }

        [Theory]
        [InlineData(new string[] {"A", "A" , "A" , "A" , "B", "B", "B", "D" },270)]
        [InlineData(new string[] { "A", "A", "B", "B", "B", "B", "D", "D", "D","C" }, 255)]
        [InlineData(new string[] { "A", "B", "C", "D"}, 115)]
        [InlineData(new string[] {  }, 0)]
        public void ReturnTotalPriceWhenVariousItemsScannedAsActualTotalPricePercisionTest(string[] itemsToBeScanned,decimal expectedTotalPrice)
        {
            Checkout checkout = new Checkout(Rules);

            foreach (string item in itemsToBeScanned)
            {
                checkout.Scan(item);
            }

            Assert.Equal(expectedTotalPrice, checkout.GetTotalPrice());
        }
    }
}