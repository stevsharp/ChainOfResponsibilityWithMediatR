namespace ChainOfResponsibility.Application.Abstractions.Time;

public sealed class SystemClockService : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}