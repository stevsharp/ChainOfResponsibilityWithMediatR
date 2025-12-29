namespace ChainOfResponsibility.Application.Abstractions.Persistence.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
}
