﻿namespace Ordering.Domain.Exceptions;

public class DomainException : Exception
{
    
    public DomainException() : base("An error occurred in the domain layer.")
    {
    }
    public DomainException(string message) : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
