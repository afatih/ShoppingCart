namespace ShoppingCart.Models
{
    public class Product
    {
        public Product(string title, double price, Category category)
        {
            Title = title;
            Price = price;
            Category = category;
        }
        public string Title { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
    }
}
