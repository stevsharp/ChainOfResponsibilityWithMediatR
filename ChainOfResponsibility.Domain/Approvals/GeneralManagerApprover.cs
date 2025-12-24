using ChainOfResponsibility.Domain.PurchaseOrders;

namespace ChainOfResponsibility.Domain.Approvals;

public sealed class GeneralManagerApprover : IPurchaseApprover
{
    public Task<ApprovalDecision?> TryApproveAsync(PurchaseOrder po, CancellationToken ct)
    {
        return Task.FromResult<ApprovalDecision?>(new ApprovalDecision(true, "GeneralManager"));
    }
}