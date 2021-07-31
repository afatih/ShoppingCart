using ShoppingCart.Enums;
using System;

namespace ShoppingCart.Models
{
    public class Coupon
    {
        public Coupon(double minPurchaseAmount, double discountValue, DiscountType discountType)
        {
            MinPurchaseAmount = minPurchaseAmount;
            DiscountValue = discountValue;
            DiscountType = discountType;
        }

        public double MinPurchaseAmount { get; set; }
        public double DiscountValue { get; set; }
        public DiscountType DiscountType { get; set; }

        public double GetDiscountAmount(Cart cart)
        {
            if (cart.TotalAmountAfterDiscounts <= MinPurchaseAmount)
                return 0;

            var discountValue = DiscountType switch
            {
                DiscountType.Rate => cart.TotalAmountAfterDiscounts * (DiscountValue / 100),
                DiscountType.Amount => DiscountValue,
                _ => throw new NotImplementedException()
            };

            return discountValue;
        }
    }
}