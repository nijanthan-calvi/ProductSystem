namespace Product.Business.Exceptions;

public class DuplicateDataException(string message) : Exception(message)
{
    public string PropertyName { get; } = message;
}
