namespace ChainOfResponsibility.Infrastructure.Common.Exceptions;

public sealed class NotFoundException(string message) : Exception(message)
{
}
