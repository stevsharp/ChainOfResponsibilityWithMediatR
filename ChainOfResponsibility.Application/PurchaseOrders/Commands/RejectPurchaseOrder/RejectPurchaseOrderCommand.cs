using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.RejectPurchaseOrder;

public sealed record RejectPurchaseOrderCommand(
    Guid PurchaseOrderId,
    string Reason
) : IRequest;
