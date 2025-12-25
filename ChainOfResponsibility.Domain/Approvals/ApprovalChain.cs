using ChainOfResponsibility.Domain.PurchaseOrders;

namespace ChainOfResponsibility.Domain.Approvals;

public sealed class ApprovalChain(IEnumerable<IPurchaseApprover> approvers)
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IReadOnlyList<IPurchaseApprover> _approvers = [.. approvers];
    /// <summary>
    /// 
    /// </summary>
    /// <param name="po"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<ApprovalDecision> ApproveAsync(PurchaseOrder po, CancellationToken ct)
    {
        foreach (var approver in _approvers)
        {
            ct.ThrowIfCancellationRequested();

            var decision = await approver.TryApproveAsync(po, ct);
            if (decision is not null)
                return decision;
        }

        throw new InvalidOperationException("No approver configured to handle this purchase order.");
    }
}
