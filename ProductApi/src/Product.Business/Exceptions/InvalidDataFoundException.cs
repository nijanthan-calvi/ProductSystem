namespace Product.Business.Exceptions;

public class InvalidDataFoundException(string message) : Exception(message)
{
    public string PropertyName { get; } = message;
}
