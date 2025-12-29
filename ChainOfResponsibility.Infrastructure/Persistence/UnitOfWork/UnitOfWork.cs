using ChainOfResponsibility.Application.Abstractions.Persistence.UnitOfWork;

namespace ChainOfResponsibility.Infrastructure.Persistence.UnitOfWork;

public sealed class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}
