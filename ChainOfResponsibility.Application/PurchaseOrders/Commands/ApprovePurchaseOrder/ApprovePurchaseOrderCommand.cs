using ChainOfResponsibility.Application.Abstractions.Messaging;
using ChainOfResponsibility.Domain.Approvals;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;

public sealed record ApprovePurchaseOrderCommand(Guid PurchaseOrderId)
    : ICommand<ApprovalDecision>;
