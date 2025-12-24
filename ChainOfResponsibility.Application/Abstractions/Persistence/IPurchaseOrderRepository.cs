using ChainOfResponsibility.Domain.PurchaseOrders;

namespace ChainOfResponsibility.Application.Abstractions.Persistence;

public interface IPurchaseOrderRepository
{
    ValueTask<PurchaseOrder?> GetAsync(Guid id, CancellationToken ct);

    Task<(IReadOnlyList<PurchaseOrder> items, int total)> ListAsync(int page, int pageSize ,CancellationToken ct);

    void Add(PurchaseOrder po);

    Task SaveChangesAsync(CancellationToken ct);
}