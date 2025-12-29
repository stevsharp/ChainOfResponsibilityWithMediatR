using FluentValidation;

namespace ChainOfResponsibility.Application.PurchaseOrders.Commands.RejectPurchaseOrder;

public sealed class RejectPurchaseOrderValidator : AbstractValidator<RejectPurchaseOrderCommand>
{
    public RejectPurchaseOrderValidator()
    {
        RuleFor(x => x.PurchaseOrderId)
            .NotEmpty();

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(400);
    }
}

