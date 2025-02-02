using ShoppingCartApp.Models;
using ShoppingCartApp.Exceptions;
using System.Text.Json;

namespace ShoppingCartApp.Services
{
    /// <summary>
    /// Manages a shopping cart that holds a collection of products.
    /// </summary>
    public class ShoppingCart
    {
        private List<Product> _products = new List<Product>();

        /// <summary>
        /// Adds a product to the shopping cart.
        /// </summary>
        /// <param name="product">The product to be added.</param>
        /// <exception cref="DuplicateProductException">Thrown when a product with the same ID already exists in the cart.</exception>
        public void AddProduct(Product product)
        {
            if (_products.Any(p => p.Id == product.Id))
                throw new DuplicateProductException($"Product with ID {product.Id} already exists.");

            _products.Add(product);
            Console.WriteLine($"Product {product.Name} added.");
        }

        /// <summary>
        /// Removes a product from the shopping cart based on its ID.
        /// </summary>
        /// <param name="productId">The ID of the product to be removed.</param>
        public void RemoveProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                _products.Remove(product);
                Console.WriteLine($"Product {product.Name} removed.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        /// <summary>
        /// Displays all products in the shopping cart.
        /// </summary>
        public void DisplayProducts()
        {
            if (_products.Count == 0)
            {
                Console.WriteLine("No products in the cart.");
                return;
            }

            Console.WriteLine("\nProducts in Cart:");
            foreach (var product in _products)
                product.DisplayDetails();
        }

        /// <summary>
        /// Calculates and returns the total price of all products in the cart.
        /// </summary>
        /// <returns>The total price of all products.</returns>
        public decimal GetTotalPrice()
        {
            return _products.Sum(p => p.Price);
        }

        /// <summary>
        /// Saves the shopping cart to a file using JSON serialization.
        /// </summary>
        /// <param name="filePath">The file path where the shopping cart data will be saved.</param>
        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(_products);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Shopping cart saved.");
        }

        /// <summary>
        /// Loads the shopping cart data from a file using JSON deserialization.
        /// </summary>
        /// <param name="filePath">The file path from which to load the shopping cart data.</param>
        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                _products = JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
                Console.WriteLine("Shopping cart loaded.");
            }
            else
            {
                Console.WriteLine("No saved cart found.");
            }
        }

        /// <summary>
        /// Filters products by a given price range.
        /// </summary>
        /// <param name="minPrice">The minimum price in the range.</param>
        /// <param name="maxPrice">The maximum price in the range.</param>
        /// <returns>A list of products within the specified price range.</returns>
        public List<Product> FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }
    }
}
