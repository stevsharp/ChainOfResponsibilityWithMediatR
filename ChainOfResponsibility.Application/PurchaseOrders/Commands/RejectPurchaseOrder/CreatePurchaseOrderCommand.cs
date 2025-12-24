using ChainOfResponsibility.Application.Abstractions.Exceptions;
using ChainOfResponsibility.Application.Abstractions.Persistence;
using ChainOfResponsibility.Application.Abstractions.Time;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.RejectPurchaseOrder;
/// <summary>
/// 
/// </summary>
public sealed class CreatePurchaseOrderCommand(IPurchaseOrderRepository repo, IClock clock) : IRequestHandler<RejectPurchaseOrderCommand>
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IPurchaseOrderRepository _repo = repo;
    /// <summary>
    /// 
    /// </summary>
    private readonly IClock _clock = clock;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task Handle(RejectPurchaseOrderCommand request, CancellationToken ct)
    {
        var po = await _repo.GetAsync(request.PurchaseOrderId, ct) ?? throw new NotFoundException($"PurchaseOrder '{request.PurchaseOrderId}' not found.");

        po.MarkRejected(request.Reason, _clock.UtcNow);

        await _repo.SaveChangesAsync(ct);
    }
}