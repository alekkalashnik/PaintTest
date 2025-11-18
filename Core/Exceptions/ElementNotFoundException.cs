namespace PaintTest.Core.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public string ElementIdentifier { get; }

        public ElementNotFoundException(string message) : base(message)
        {
            ElementIdentifier = string.Empty;
        }

        public ElementNotFoundException(string message, string elementIdentifier) : base(message)
        {
            ElementIdentifier = elementIdentifier;
        }

        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            ElementIdentifier = string.Empty;
        }
    }
}