using ChainOfResponsibility.Application.Abstractions.Messaging;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.CreatePurchaseOrder;

public sealed record CreatePurchaseOrderCommand(
    int Amount,
    decimal Price,
    string Name
) : ICommand<Guid>;
