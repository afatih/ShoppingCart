using ShoppingCart.Models;

namespace ShoppingCart.Calculators
{
    public abstract class BaseDeliveryCostCalculator
    {
        public BaseDeliveryCostCalculator(Cart cart, double costPerDelivery, double costPerProduct, double fixedCost)
        {
            Cart = cart;
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
            FixedCost = fixedCost;
        }
        public Cart Cart { get; set; }
        public double CostPerDelivery { get; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }

        public abstract double Calculate();
    }
}
