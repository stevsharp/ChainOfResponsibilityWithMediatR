namespace ChainOfResponsibility.Domain.PurchaseOrders;

public sealed class PurchaseOrder
{
    /// <summary>
    /// 
    /// </summary>
    private PurchaseOrder(){}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    /// <param name="price"></param>
    /// <param name="name"></param>
    /// <exception cref="ArgumentException"></exception>
    public PurchaseOrder(Guid id, int amount, decimal price, string name, DateTime createdAtUtc)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Name is required.", nameof(name));

        Id = id;
        Amount = amount;
        Price = price;
        Name = name.Trim();
        CreatedAtUtc = createdAtUtc;
        Status = PurchaseOrderStatus.Pending;
    }
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; private set; }
    public int Amount { get; private set; }
    public decimal Price { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public PurchaseOrderStatus Status { get; private set; }
    public string? ApprovedBy { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? ApprovedAtUtc { get; private set; }
    public DateTime? RejectedAtUtc { get; private set; }
    public string? RejectionReason { get; private set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="approvedBy"></param>
    /// <param name="approvedAtUtc"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void MarkApproved(string approvedBy, DateTime approvedAtUtc)
    {
        if (Status != PurchaseOrderStatus.Pending)
            throw new InvalidOperationException($"Cannot approve when status is {Status}.");

        if (string.IsNullOrWhiteSpace(approvedBy))
            throw new ArgumentException("ApprovedBy is required.", nameof(approvedBy));

        Status = PurchaseOrderStatus.Approved;
        ApprovedBy = approvedBy.Trim();
        ApprovedAtUtc = approvedAtUtc;
        RejectedAtUtc = null;
        RejectionReason = null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reason"></param>
    /// <param name="rejectedAtUtc"></param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public void MarkRejected(string reason, DateTime rejectedAtUtc)
    {
        if (Status != PurchaseOrderStatus.Pending)
            throw new InvalidOperationException($"Cannot reject when status is {Status}.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Rejection reason is required.", nameof(reason));

        Status = PurchaseOrderStatus.Rejected;
        RejectionReason = reason.Trim();
        RejectedAtUtc = rejectedAtUtc;

        ApprovedBy = null;
        ApprovedAtUtc = null;
    }

}
