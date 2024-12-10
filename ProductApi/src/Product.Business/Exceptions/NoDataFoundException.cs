namespace Product.Business.Exceptions;

public class NoDataFoundException(string message) : Exception(message)
{
    public string PropertyName { get; } = message;
}
