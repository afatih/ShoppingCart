namespace ShoppingCart.Models
{
    public class CartItem
    {
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double CartItemAmount => Quantity * Product.Price;
    }
}
