using Microsoft.Extensions.DependencyInjection;
using ShoppingCart.Calculators;
using ShoppingCart.Enums;
using ShoppingCart.Models;
using ShoppingCart.Services;
using ShoppingCart.Storage;
using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    internal class Program
    {
        private const double CostPerDelivery = 1.6;
        private const double CostPerProduct = 7.1;
        private const double FixedCost = 2.99;

        private static void Main(string[] args)
        {
            #region Setup our DI

            var serviceProvider = new ServiceCollection()
                .AddSingleton<CartStorage>()
                .AddScoped<ICartService, CartService>()
                .BuildServiceProvider();

            var _cartStorage = serviceProvider.GetService<CartStorage>();
            var _cartService = serviceProvider.GetService<ICartService>();

            #endregion Setup our DI

            #region Seed Data

            var food = new Category("Food");
            var clothes = new Category("Clothes");

            var apple = new Product("Apple", 10, food);
            var orange = new Product("Orange", 20, food);

            var shoe = new Product("Shoe", 100, clothes);
            var tshirt = new Product("Tshirt", 300, clothes);

            var cart = _cartStorage.Cart;
            _cartService.Add(apple, 3);
            _cartService.Add(orange, 5);
            _cartService.Add(shoe, 3);
            _cartService.Add(tshirt, 1);

            var campaign1 = new Campaign("foodCampaign1", food, 20.0, 3, DiscountType.Rate);
            var campaign2 = new Campaign("foodCampaign2", food, 50.0, 5, DiscountType.Rate);
            var campaign3 = new Campaign("foodCampaign3", food, 5, 3, DiscountType.Amount);

            var campaign4 = new Campaign("clothesCampaign1", clothes, 20.0, 3, DiscountType.Rate);
            var campaign5 = new Campaign("clothesCampaign2", clothes, 50.0, 5, DiscountType.Rate);
            var campaign6 = new Campaign("clothesCampaign3", clothes, 5, 3, DiscountType.Amount);

            var campaigns = new List<Campaign>() { campaign1, campaign2, campaign3, campaign4, campaign5, campaign6 };

            var coupon = new Coupon(100, 10, DiscountType.Rate);

            #endregion Seed Data

            #region Get Cart Details

            _cartService.GetCartDetails(campaigns, coupon);

            #endregion Get Cart Details

            #region Get Delivery Cost

            var deliveryCostCalculator = new BasicDeliveryCostCalculator(cart, CostPerDelivery, CostPerProduct, FixedCost);
            var deliveryCost = deliveryCostCalculator.Calculate();

            Console.WriteLine($"\nDelivery Cost : {deliveryCost.ToString("0.##")} TL.");

            #endregion Get Delivery Cost
        }
    }
}