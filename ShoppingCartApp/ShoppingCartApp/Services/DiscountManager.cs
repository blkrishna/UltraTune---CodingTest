using ShoppingCartApp.Models;

namespace ShoppingCartApp.Services
{
    /// <summary>
    /// Manages discount operations for products.
    /// </summary>
    public class DiscountManager
    {
        /// <summary>
        /// Applies a percentage discount to products above a specified price threshold.
        /// </summary>
        /// <param name="cart">The shopping cart containing products.</param>
        /// <param name="threshold">The price threshold above which a discount is applied.</param>
        /// <param name="discountPercentage">The discount percentage to apply.</param>
        public static decimal ApplyPercentageDiscount(Product product, decimal percentage)
        {
            return product.Price - (product.Price * (percentage / 100));
        }

        /// <summary>
        /// Applies a Fixed amount Discount to products.
        /// </summary>
        /// <param name="product"></param>
        /// <param name="discountAmount"></param>
        /// <returns></returns>
        public static decimal ApplyFixedDiscount(Product product, decimal discountAmount)
        {
            return product.Price - discountAmount < 0 ? 0 : product.Price - discountAmount;
        }
    }
}
