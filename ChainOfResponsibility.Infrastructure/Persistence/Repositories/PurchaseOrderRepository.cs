using ChainOfResponsibility.Application.Abstractions.Persistence;
using ChainOfResponsibility.Domain.PurchaseOrders;

using Microsoft.EntityFrameworkCore;

namespace ChainOfResponsibility.Infrastructure.Persistence.Repositories;

/// <summary>
/// 
/// </summary>
/// <param name="db"></param>
public sealed class PurchaseOrderRepository(AppDbContext db) : IPurchaseOrderRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public ValueTask<PurchaseOrder?> GetAsync(Guid id, CancellationToken ct)
        => db.PurchaseOrders.FindAsync([id] , ct);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="po"></param>
    public void Add(PurchaseOrder po) => db.PurchaseOrders.Add(po);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public Task SaveChangesAsync(CancellationToken ct)
        => db.SaveChangesAsync(ct);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<(IReadOnlyList<PurchaseOrder> items, int total)> ListAsync(int page, int pageSize, CancellationToken ct)
    {
        var query = db.PurchaseOrders.AsNoTracking().OrderByDescending(x => x.CreatedAtUtc);

        var total = await query.CountAsync(ct);

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(ct);

        return (items, total);
    }
}

