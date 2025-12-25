using ChainOfResponsibility.Application.Abstractions.Persistence;
using ChainOfResponsibility.Domain.PurchaseOrders;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Queries.GetPurchaseOrderById;

public sealed class GetPurchaseOrderByIdHandler(IPurchaseOrderRepository repo)
        : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrder?>
{
    public Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery request, CancellationToken ct)
        => repo.GetAsync(request.Id, ct).AsTask();

}