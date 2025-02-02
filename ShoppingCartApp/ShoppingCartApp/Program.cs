using ShoppingCartApp.Models;
using ShoppingCartApp.Services;

namespace ShoppingCartApp
{
    /// <summary>
    /// Entry point for the shopping cart console application.
    /// </summary>
    class Program
    {
        static void Main()
        {
            ShoppingCart cart = new ShoppingCart();
            Order order = new Order();
            string filePath = "shoppingcart.json";

            cart.LoadFromFile(filePath);

            while (true)
            {
                Console.WriteLine("\n1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Display Products");
                Console.WriteLine("4. Apply Discount");
                Console.WriteLine("5. Create Order");
                Console.WriteLine("6. Save Cart");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Product ID: ");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Product Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Product Price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        cart.AddProduct(new Product(id, name, price));
                        break;

                    case "2":
                        Console.Write("Enter Product ID to remove: ");
                        int removeId = int.Parse(Console.ReadLine());
                        cart.RemoveProduct(removeId);
                        break;

                    case "3":
                        cart.DisplayProducts();
                        break;

                    case "4":
                        Console.Write("Enter Product ID for discount: ");
                        int discountId = int.Parse(Console.ReadLine());
                        var product = cart.FilterByPriceRange(0, decimal.MaxValue).FirstOrDefault(p => p.Id == discountId);

                        if (product != null)
                        {
                            Console.Write("Enter discount percentage: ");
                            decimal percent = decimal.Parse(Console.ReadLine());
                            Console.WriteLine($"New Price: {DiscountManager.ApplyPercentageDiscount(product, percent):C}");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                        break;

                    case "5":
                        Console.Write("Enter Product ID to add to order: ");
                        int orderId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        var orderProduct = cart.FilterByPriceRange(0, decimal.MaxValue).FirstOrDefault(p => p.Id == orderId);
                        if (orderProduct != null)
                        {
                            order.AddProduct(orderProduct, quantity);
                            Console.WriteLine("Product added to order.");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }
                        break;

                    case "6":
                        cart.SaveToFile(filePath);
                        break;

                    case "7":
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }
    }
}
