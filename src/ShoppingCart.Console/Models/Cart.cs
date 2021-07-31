using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get; set; }
        public double TotalAmountAfterDiscounts { get; set; }
        public double TotalAmount => CartItems.Sum(c => c.CartItemAmount);
    }
}