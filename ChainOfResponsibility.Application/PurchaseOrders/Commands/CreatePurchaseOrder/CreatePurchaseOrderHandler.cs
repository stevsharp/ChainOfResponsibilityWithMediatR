using ChainOfResponsibility.Application.Abstractions.Persistence;
using ChainOfResponsibility.Application.Abstractions.Time;
using ChainOfResponsibility.Domain.PurchaseOrders;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.CreatePurchaseOrder;

public sealed class CreatePurchaseOrderHandler(IPurchaseOrderRepository repo, IClock clock)
        : IRequestHandler<CreatePurchaseOrderCommand, Guid>
{
    private readonly IPurchaseOrderRepository _repo = repo;
    private readonly IClock _clock = clock;

    public async Task<Guid> Handle(CreatePurchaseOrderCommand request, CancellationToken ct)
    {
        var id = Guid.NewGuid();

        var po = new PurchaseOrder(
            id: id,
            amount: request.Amount,
            price: request.Price,
            name: request.Name,
            createdAtUtc: _clock.UtcNow
        );

        _repo.Add(po);

        await _repo.SaveChangesAsync(ct);

        return id;
    }
}
