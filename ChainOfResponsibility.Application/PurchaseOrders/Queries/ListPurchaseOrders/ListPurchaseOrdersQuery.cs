using ChainOfResponsibility.Application.Abstractions.Messaging;

namespace ChainOfResponsibility.Application.PurchaseOrders.Queries.ListPurchaseOrders;

public sealed record ListPurchaseOrdersQuery(int Page, int PageSize)
    : IQuery<ListPurchaseOrdersResult>;
