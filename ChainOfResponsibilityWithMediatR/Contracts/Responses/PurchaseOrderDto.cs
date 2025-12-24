namespace ChainOfResponsibility.Api.Contracts.Responses;

public sealed record PurchaseOrderDto(
    Guid Id,
    int Amount,
    decimal Price,
    string Name,
    string Status,
    string? ApprovedBy,
    DateTime CreatedAtUtc,
    DateTime? ApprovedAtUtc,
    DateTime? RejectedAtUtc,
    string? RejectionReason
);