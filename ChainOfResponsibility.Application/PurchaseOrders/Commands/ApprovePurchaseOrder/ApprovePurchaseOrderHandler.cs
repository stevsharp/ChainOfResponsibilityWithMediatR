using ChainOfResponsibility.Application.Abstractions.Exceptions;
using ChainOfResponsibility.Application.Abstractions.Persistence.Repositories;
using ChainOfResponsibility.Application.Abstractions.Time;
using ChainOfResponsibility.Domain.Approvals;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;

public sealed class ApprovePurchaseOrderHandler(IPurchaseOrderRepository repo, ApprovalChain chain, IClock clock)
        : IRequestHandler<ApprovePurchaseOrderCommand, ApprovalDecision>
{
    private readonly IPurchaseOrderRepository _repo = repo;
    private readonly ApprovalChain _chain = chain;
    private readonly IClock _clock = clock;

    public async Task<ApprovalDecision> Handle(ApprovePurchaseOrderCommand request, CancellationToken ct)
    {
        var po = await _repo.GetAsync(request.PurchaseOrderId, ct) ?? 
            throw new NotFoundException($"PurchaseOrder '{request.PurchaseOrderId}' not found.");

        var decision = await _chain.ApproveAsync(po, ct);

        po.MarkApproved(decision.ApprovedBy, _clock.UtcNow);

        return decision;
    }
}
