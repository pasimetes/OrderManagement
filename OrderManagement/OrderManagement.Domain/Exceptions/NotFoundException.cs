﻿namespace OrderManagement.Domain.Exceptions
{
    public class NotFoundException(string message = "", Exception innerException = null)
        : Exception(message, innerException)
    {
    }
}
