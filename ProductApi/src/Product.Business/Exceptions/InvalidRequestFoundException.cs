namespace Product.Business.Exceptions;

public class InvalidRequestFoundException(string message) : Exception(message)
{
    public string PropertyName { get; } = message;
}
