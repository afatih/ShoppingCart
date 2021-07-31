using ShoppingCart.Models;
using System.Collections.Generic;

namespace ShoppingCart.Services
{
    public interface ICartService
    {
        public void Add(Product product, int quantity);

        /// <summary>
        /// Returns the campaign and discount amount that applies the most discount to the cart is determined
        /// </summary>
        /// <param name="category">Selected category</param>
        /// <param name="campaigns">Selected category campaigns</param>
        /// <returns>(Discount amount, Campaign name)</returns>
        public (double, string) GetCampaignDiscount(Category category, List<Campaign> campaigns);

        /// <summary>
        /// Applies discount that get from campaign has most discount to the cart
        /// </summary>
        /// <param name="campaigns">All Campaigns</param>
        public void ApplyCampaignDiscounts(List<Campaign> campaigns);

        /// <summary>
        /// Return coupon discount to the cart is determined
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public double GetCouponDiscount(Coupon coupon);

        /// <summary>
        /// Apply discount that get from coupon to the cart
        /// </summary>
        /// <param name="coupon"></param>
        public void ApplyCouponDiscount(Coupon coupon);

        /// <summary>
        /// Return cart details
        /// </summary>
        /// <param name="campaigns">Campaigns</param>
        /// <param name="coupon">Coupon</param>
        public void GetCartDetails(List<Campaign> campaigns, Coupon coupon);
    }
}
