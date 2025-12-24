namespace ChainOfResponsibility.Domain.Approvals;

public interface IPurchaseApprover
{
    Task<ApprovalDecision?> TryApproveAsync(PurchaseOrders.PurchaseOrder po, CancellationToken ct);
}
