namespace ShoppingCartApp.Exceptions
{
    /// <summary>
    /// Exception thrown when a duplicate product is added to the shopping cart.
    /// </summary>
    public class DuplicateProductException : Exception
    {
        public DuplicateProductException(string message) : base(message) { }
    }
}
