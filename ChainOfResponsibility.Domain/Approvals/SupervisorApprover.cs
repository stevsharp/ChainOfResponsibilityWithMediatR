using ChainOfResponsibility.Domain.PurchaseOrders;
using ChainOfResponsibility.Domain.Specifications;

namespace ChainOfResponsibility.Domain.Approvals;

public sealed class SupervisorApprover : IPurchaseApprover
{
    public Task<ApprovalDecision?> TryApproveAsync(PurchaseOrder po, CancellationToken ct)
    {
        var spec = new Specification<PurchaseOrder>(x => x.Price < 200m)
            .And(new Specification<PurchaseOrder>(x => x.Amount <= 2));

        if (!spec.IsSatisfiedBy(po))
            return Task.FromResult<ApprovalDecision?>(null);

        return Task.FromResult<ApprovalDecision?>(new ApprovalDecision(true, "Supervisor"));
    }
}