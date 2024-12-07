namespace CheckoutKata
{
    public class PricingRule
    {
        public string SKU { get; set; }
        public int SinglePrice { get; set; }
        public int SpecialOfferPrice { get; set; }
        public int SpecialOfferQuantity { get; set; }
    }
}