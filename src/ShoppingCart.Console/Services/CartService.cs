using ConsoleTables;
using ShoppingCart.Models;
using ShoppingCart.Storage;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Services
{
    public class CartService : ICartService
    {
        private readonly CartStorage _cartStorage;

        public CartService(CartStorage cartStorage)
        {
            _cartStorage = cartStorage;
        }

        public void Add(Product product, int quantity)
        {
            _cartStorage.Cart.CartItems.Add(new CartItem(product, quantity));
        }

        public (double, string) GetCampaignDiscount(Category category, List<Campaign> campaigns)
        {
            var maxCampaignDiscount = 0.0;
            var campaignName = "There is no available campaign";

            //Return 0 if there is no available  campaign for selected category
            if (!campaigns.Any())
                return (maxCampaignDiscount, campaignName);

            var categoryItems = _cartStorage.Cart.CartItems.Where(c => c.Product.Category == category).ToList();
            foreach (var campaign in campaigns)
            {
                var discountAmount = campaign.GetDiscountAmount(categoryItems);
                if (discountAmount > maxCampaignDiscount)
                {
                    maxCampaignDiscount = discountAmount;
                    campaignName = campaign.Name;
                }
            }
            return (maxCampaignDiscount, campaignName);
        }

        public void ApplyCampaignDiscounts(List<Campaign> campaigns)
        {
            _cartStorage.Cart.TotalAmountAfterDiscounts = _cartStorage.Cart.TotalAmount;

            var categories = _cartStorage.Cart.CartItems.Select(c => c.Product.Category).Distinct();
            foreach (var category in categories)
            {
                var (categoryCampaignDiscount, campaignName) = GetCampaignDiscount(category, campaigns.Where(c => c.Category == category).ToList());
                _cartStorage.Cart.TotalAmountAfterDiscounts -= categoryCampaignDiscount;
            }
        }

        public double GetCouponDiscount(Coupon coupon)
        {
            return coupon.GetDiscountAmount(_cartStorage.Cart);
        }

        public void ApplyCouponDiscount(Coupon coupon)
        {
            var couponDiscount = GetCouponDiscount(coupon);
            _cartStorage.Cart.TotalAmountAfterDiscounts -= couponDiscount;
        }

        public void GetCartDetails(List<Campaign> campaigns, Coupon coupon)
        {
            System.Console.WriteLine($"\nCart Details\n");

            var table = new ConsoleTable("CategoryName", "ProductName", "Quantity", "UnitPrice", "TotalPrice");

            var categories = _cartStorage.Cart.CartItems.Select(c => c.Product.Category).Distinct();
            foreach (var category in categories)
            {
                var categoryCartItems = _cartStorage.Cart.CartItems.Where(c => c.Product.Category == category);

                foreach (var cartItem in categoryCartItems)
                {
                    table.AddRow(category.Title, cartItem.Product.Title, cartItem.Quantity, cartItem.Product.Price, cartItem.CartItemAmount);
                }
            }
            table.Write();
            System.Console.WriteLine($"\nCart Total Amount : {_cartStorage.Cart.TotalAmount}");

            foreach (var category in categories)
            {
                var (campaignDiscount, campaignName) = GetCampaignDiscount(category, campaigns.Where(c => c.Category == category).ToList());
                System.Console.WriteLine($"\n'{category.Title}' Category Campaign Discount Amount : {campaignDiscount} TL, Campaign Name : {campaignName}");
            }
            ApplyCampaignDiscounts(campaigns);

            System.Console.WriteLine($"\nCoupon Discount Amount  : {GetCouponDiscount(coupon)} TL.");
            ApplyCouponDiscount(coupon);

            System.Console.WriteLine($"\nCart Total Amount After Discounts : {_cartStorage.Cart.TotalAmountAfterDiscounts} TL.");
        }
    }
}