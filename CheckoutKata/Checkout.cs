
using System.Data;

namespace CheckoutKata
{
    public class Checkout
    {
        private List<PricingRule> _rules;
        private List<string> _basket;

        public Checkout(List<PricingRule> rules)
        {
            _rules = rules;
            _basket=new List<string>();
        }

        public void Scan(string item)
        {
            _basket.Add(item);
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in _basket)
            {
                PricingRule rule = _rules.FirstOrDefault(e => string.Compare(e.SKU, item, true) == 0);
                totalPrice += rule.SinglePrice;
            }
            return totalPrice;
        }
    }
}