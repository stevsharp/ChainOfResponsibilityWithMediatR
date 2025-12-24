using ChainOfResponsibility.Application.Abstractions.Exceptions;
using ChainOfResponsibility.Application.Abstractions.Persistence;
using ChainOfResponsibility.Application.Abstractions.Time;
using ChainOfResponsibility.Domain.Approvals;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;

public sealed record ApprovePurchaseOrderCommand(Guid PurchaseOrderId)
    : IRequest<ApprovalDecision>;

public sealed class ApprovePurchaseOrderHandler
    : IRequestHandler<ApprovePurchaseOrderCommand, ApprovalDecision>
{
    private readonly IPurchaseOrderRepository _repo;
    private readonly ApprovalChain _chain;
    private readonly IClock _clock;

    public ApprovePurchaseOrderHandler(IPurchaseOrderRepository repo, ApprovalChain chain, IClock clock)
    {
        _repo = repo;
        _chain = chain;
        _clock = clock;
    }

    public async Task<ApprovalDecision> Handle(ApprovePurchaseOrderCommand request, CancellationToken ct)
    {
        var po = await _repo.GetAsync(request.PurchaseOrderId, ct);

        if (po is null)
            throw new NotFoundException($"PurchaseOrder '{request.PurchaseOrderId}' not found.");

        var decision = await _chain.ApproveAsync(po, ct);

        po.MarkApproved(decision.ApprovedBy, _clock.UtcNow);

        await _repo.SaveChangesAsync(ct);

        return decision;
    }
}
