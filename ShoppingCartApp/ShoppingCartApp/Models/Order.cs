namespace ShoppingCartApp.Models
{
    /// <summary>
    /// Represents an order containing a list of products and their quantities.
    /// </summary>
    public class Order
    {
        public List<(Product Product, int Quantity)> OrderedProducts { get; set; } = new List<(Product, int)>();

        /// <summary>
        /// Adds a product to the order.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <param name="quantity">The quantity of the product.</param>
        public void AddProduct(Product product, int quantity)
        {
            OrderedProducts.Add((product, quantity));
        }

        /// <summary>
        /// Calculates the total price of the order.
        /// </summary>
        /// <returns>The total order price.</returns>
        public decimal GetTotalOrderPrice()
        {
            return OrderedProducts.Sum(item => item.Product.Price * item.Quantity);
        }

        /// <summary>
        /// Displays the order details including products, quantities, and total price.
        /// </summary>
        public void DisplayOrderDetails()
        {
            Console.WriteLine("\nOrder Details:");
            foreach (var item in OrderedProducts)
            {
                Console.WriteLine($"Product: {item.Product.Name}, Quantity: {item.Quantity}, Total: {item.Product.Price * item.Quantity:C}");
            }
            Console.WriteLine($"Total Order Price: {GetTotalOrderPrice():C}");
        }
    }
}
