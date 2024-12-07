
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
            PricingRule rule = _rules.FirstOrDefault(e => string.Compare(e.SKU, _basket[0], true) == 0);
            return rule.SinglePrice;
        }
    }
}