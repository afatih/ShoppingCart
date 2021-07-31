using ShoppingCart.Models;
using System.Linq;

namespace ShoppingCart.Calculators
{
    public class BasicDeliveryCostCalculator : BaseDeliveryCostCalculator
    {
        public BasicDeliveryCostCalculator(Cart cart, double costPerDelivery, double costPerProduct, double fixedCost)
            : base(cart, costPerDelivery, costPerProduct, fixedCost)
        {

        }
        public override double Calculate()
        {
            var numberOfDeliveries = Cart.CartItems.Select(c => c.Product.Category).Distinct().Count();
            var numberOfProduct = Cart.CartItems.Select(c => c.Product).Distinct().Count();

            return (CostPerDelivery * numberOfDeliveries) + (CostPerProduct * numberOfProduct) + FixedCost;
        }
    }
}
