using Carter;

using ChainOfResponsibility.Api.Contracts.Requests;
using ChainOfResponsibility.Api.Contracts.Responses;
using ChainOfResponsibility.Application.PurchaseOrders.Commands.ApprovePurchaseOrder;
using ChainOfResponsibility.Application.PurchaseOrders.Commands.RejectPurchaseOrder;
using ChainOfResponsibility.Application.PurchaseOrders.Queries.GetPurchaseOrderById;
using ChainOfResponsibility.Application.PurchaseOrders.Queries.ListPurchaseOrders;
using ChainOfResponsibility.Domain.PurchaseOrders;

using MediatR;

namespace ChainOfResponsibility.Api.Endpoints;

public sealed class PurchaseOrdersModule : CarterModule
{
    public PurchaseOrdersModule()
    {
        WithTags("PurchaseOrders");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="NotImplementedException"></exception>
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreatePurchaseOrderRequest body, IMediator mediator, CancellationToken ct) =>
        {
            var id = await mediator.Send(new Application.PurchaseOrders.Commands.CreatePurchaseOrder.CreatePurchaseOrderCommand(body.Amount, body.Price, body.Name), ct);

            return Results.Created($"/api/purchase-orders/{id}", new { id });
        });

        app.MapGet("/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var po = await mediator.Send(new GetPurchaseOrderByIdQuery(id), ct);

            return po is null
                ? Results.NotFound()
                : Results.Ok(ToDto(po));
        });

        app.MapGet("/", async (int? page, int? pageSize, IMediator mediator, CancellationToken ct) =>
        {

            var pe = page.GetValueOrDefault(1);
            var pg = pageSize.GetValueOrDefault(20);

            var result = await mediator.Send(new ListPurchaseOrdersQuery(pe,pg), ct);

            var dtoItems = result.Items.Select(ToDto).ToList();

            var dto = new PagedResult<PurchaseOrderDto>(
                result.Page,
                result.PageSize,
                result.Total,
                dtoItems
            );

            return Results.Ok(dto);
        });

        app.MapPost("/{id:guid}/approve", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var decision = await mediator.Send(new ApprovePurchaseOrderCommand(id), ct);

            var dto = new ApprovalDecisionDto(decision.Approved, decision.ApprovedBy);

            return Results.Ok(dto);
        });

        // Reject
        app.MapPost("/{id:guid}/reject", async (Guid id, RejectPurchaseOrderRequest body, IMediator mediator, CancellationToken ct) =>
        {
            await mediator.Send(new RejectPurchaseOrderCommand(id, body.Reason), ct);
            return Results.NoContent();
        });

    }
    private static PurchaseOrderDto ToDto(PurchaseOrder po)
       => new(
           Id: po.Id,
           Amount: po.Amount,
           Price: po.Price,
           Name: po.Name,
           Status: po.Status.ToString(),
           ApprovedBy: po.ApprovedBy,
           CreatedAtUtc: po.CreatedAtUtc,
           ApprovedAtUtc: po.ApprovedAtUtc,
           RejectedAtUtc: po.RejectedAtUtc,
           RejectionReason: po.RejectionReason
       );
}

