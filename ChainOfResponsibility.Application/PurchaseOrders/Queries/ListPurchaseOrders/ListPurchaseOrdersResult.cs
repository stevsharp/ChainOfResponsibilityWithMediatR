using ChainOfResponsibility.Domain.PurchaseOrders;

namespace ChainOfResponsibility.Application.PurchaseOrders.Queries.ListPurchaseOrders;

public sealed record ListPurchaseOrdersResult(
    int Page,
    int PageSize,
    int Total,
    IReadOnlyList<PurchaseOrder> Items
);
