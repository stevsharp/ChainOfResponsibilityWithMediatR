using ChainOfResponsibility.Domain.PurchaseOrders;
using ChainOfResponsibility.Domain.Specifications;

namespace ChainOfResponsibility.Domain.Approvals;

public sealed class EmployeeApprover : IPurchaseApprover
{
    public Task<ApprovalDecision?> TryApproveAsync(PurchaseOrder po, CancellationToken ct)
    {
        var spec = new Specification<PurchaseOrder>(x => x.Price < 100m)
            .And(new Specification<PurchaseOrder>(x => x.Amount == 1));

        if (!spec.IsSatisfiedBy(po))
            return Task.FromResult<ApprovalDecision?>(null);

        return Task.FromResult<ApprovalDecision?>(new ApprovalDecision(true, "Employee"));
    }
}