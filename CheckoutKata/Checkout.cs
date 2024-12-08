
using System.Data;

namespace CheckoutKata
{
    public class Checkout: ICheckout
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
            if (string.IsNullOrEmpty(item) || !_rules.Any(e => string.Compare(e.SKU, item, true)==0))
            {
                return;
            }
            _basket.Add(item);
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;

            Dictionary<string,int> itemsCountInBasket = new Dictionary<string,int>();
            foreach (var item in _basket)
            {
                itemsCountInBasket[item] = itemsCountInBasket.ContainsKey(item)? itemsCountInBasket[item]+1:1;
            }

             
            foreach (var itemCountInBasket in itemsCountInBasket)
            {
                PricingRule rule = _rules.FirstOrDefault(e => string.Compare(e.SKU, itemCountInBasket.Key, true) == 0);
                if (rule.SpecialOfferQuantity != null && rule.SpecialOfferQuantity > 0 && rule.SpecialOfferPrice != null)
                {
                    totalPrice += (int)(itemCountInBasket.Value / rule.SpecialOfferQuantity) * rule.SpecialOfferPrice;
                    totalPrice += (int)(itemCountInBasket.Value % rule.SpecialOfferQuantity) * rule.SinglePrice;
                }
                else
                {
                    totalPrice += itemCountInBasket.Value* rule.SinglePrice;
                }
            }
            return totalPrice;
        }
    }
}