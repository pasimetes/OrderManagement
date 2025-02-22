namespace OrderManagement.Domain.Exceptions
{
    public class BusinessException(string message = "", Exception innerException = null)
        : Exception(message, innerException)
    {
    }
}
