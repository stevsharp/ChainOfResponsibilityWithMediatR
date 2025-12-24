using ChainOfResponsibility.Application.Abstractions.Messaging;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.RejectPurchaseOrder;

public sealed record RejectPurchaseOrderCommand(
    Guid PurchaseOrderId,
    string Reason
) : ICommand;
