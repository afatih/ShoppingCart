using ShoppingCart.Models;

namespace ShoppingCart.Storage
{
    public class CartStorage
    {
        public CartStorage()
        {
            if (Cart == null)
            {
                Cart = new Cart();
            }
        }
        public Cart Cart { get; set; }
    }
}
