using ChainOfResponsibility.Application.Abstractions.Persistence;

using MediatR;

namespace ChainOfResponsibility.Application.PurchaseOrders.Queries.ListPurchaseOrders;
/// <summary>
/// 
/// </summary>
/// <param name="repo"></param>
public sealed class ListPurchaseOrdersHandler(IPurchaseOrderRepository repo)
        : IRequestHandler<ListPurchaseOrdersQuery, ListPurchaseOrdersResult>
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IPurchaseOrderRepository _repo = repo;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<ListPurchaseOrdersResult> Handle(ListPurchaseOrdersQuery request, CancellationToken ct)
    {
        var page = request.Page <= 0 ? 1 : request.Page;

        var pageSize = request.PageSize <= 0 ? 20 : Math.Min(request.PageSize, 200);

        var (items, total) = await _repo.ListAsync(page, pageSize, ct);

        return new ListPurchaseOrdersResult(
            Page: page,
            PageSize: pageSize,
            Total: total,
            Items: items
        );
    }
}
