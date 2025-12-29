

using ChainOfResponsibility.Application.Abstractions.Messaging;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace ChainOfResponsibility.Application.Common.Behaviors;

public sealed class TransactionBehavior<TRequest, TResponse>(DbContext db)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly DbContext _db = db;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        var isCommand = request is ICommand || ImplementsGenericCommand(request);

        if (!isCommand)
            return await next(ct);

        if (_db.Database.CurrentTransaction is not null)
            return await next(ct);

        await using var tx = await _db.Database.BeginTransactionAsync(ct);

        try
        {
            var response = await next(ct);
            await tx.CommitAsync(ct);
            return response;
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }

    private static bool ImplementsGenericCommand(object request)
        => request.GetType().GetInterfaces().Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(ICommand<>));
}

