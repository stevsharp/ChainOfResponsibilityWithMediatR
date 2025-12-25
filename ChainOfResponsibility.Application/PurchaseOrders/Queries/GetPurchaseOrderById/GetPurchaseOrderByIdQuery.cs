using ChainOfResponsibility.Application.Abstractions.Messaging;
using ChainOfResponsibility.Domain.PurchaseOrders;

namespace ChainOfResponsibility.Application.PurchaseOrders.Queries.GetPurchaseOrderById;

public sealed record GetPurchaseOrderByIdQuery(Guid Id) : IQuery<PurchaseOrder?>;
