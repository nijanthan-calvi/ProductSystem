namespace Product.Business.Exceptions;

public class UnauthorizedDataException(string message) : Exception(message)
{
    public string PropertyName { get; } = message;
}
