using ShoppingCart.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Models
{
    public class Campaign
    {
        public Campaign(string name, Category category, double campaignValue, int minProductCount, DiscountType discountType)
        {
            Name = name;
            Category = category;
            CampaignValue = campaignValue;
            MinProductCount = minProductCount;
            DiscountType = discountType;
        }

        public string Name { get; set; }
        public Category Category { get; set; }
        public double CampaignValue { get; set; }
        public int MinProductCount { get; set; }
        public DiscountType DiscountType { get; set; }

        public double GetDiscountAmount(List<CartItem> cartItems)
        {
            var totalAmount = cartItems.Sum(c => c.CartItemAmount);
            var productCount = cartItems.Sum(c => c.Quantity);

            if (productCount <= MinProductCount)
                return 0;

            var discountValue = DiscountType switch
            {
                DiscountType.Rate => totalAmount * (CampaignValue / 100),
                DiscountType.Amount => 5,
                _ => throw new NotImplementedException()
            };

            return discountValue;
        }
    }
}