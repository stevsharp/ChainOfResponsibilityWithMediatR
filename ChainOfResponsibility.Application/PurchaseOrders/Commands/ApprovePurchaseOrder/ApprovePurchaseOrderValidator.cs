using FluentValidation;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;

public sealed class ApprovePurchaseOrderValidator : AbstractValidator<ApprovePurchaseOrderCommand>
{
    public ApprovePurchaseOrderValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty();
    }
}
