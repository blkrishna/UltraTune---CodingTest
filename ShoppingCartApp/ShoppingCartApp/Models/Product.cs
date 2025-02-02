namespace ShoppingCartApp.Models
{
    /// <summary>
    /// Represents a product with an ID, name, and price.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Displays the details of the product.
        /// </summary>
        public void DisplayDetails()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}, Price: {Price:C}");
        }
    }
}
