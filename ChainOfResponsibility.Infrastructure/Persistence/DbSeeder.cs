using ChainOfResponsibility.Domain.PurchaseOrders;

using Microsoft.EntityFrameworkCore;

namespace ChainOfResponsibility.Infrastructure.Persistence;

using ChainOfResponsibility.Domain.PurchaseOrders;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken ct = default)
    {
        // 1) Create table + indexes (no migrations)
        await CreateSchemaAsync(db, ct);

        // 2) Seed only if empty
        if (await db.PurchaseOrders.AnyAsync(ct))
            return;

        var now = DateTime.UtcNow;

        var po1 = new PurchaseOrder(Guid.NewGuid(), 1, 50m, "Demo Order A", now.AddDays(-3));
        var po2 = new PurchaseOrder(Guid.NewGuid(), 1, 150m, "Demo Order B", now.AddDays(-2));
        var po3 = new PurchaseOrder(Guid.NewGuid(), 2, 180m, "Demo Order C", now.AddDays(-1));
        var po4 = new PurchaseOrder(Guid.NewGuid(), 5, 450m, "Demo Order D", now.AddHours(-8));
        var po5 = new PurchaseOrder(Guid.NewGuid(), 1, 90m, "Demo Order E", now.AddHours(-2));

        po2.MarkApproved("Supervisor", now.AddDays(-2).AddMinutes(15));
        po3.MarkRejected("Demo rejection, budget issue", now.AddDays(-1).AddMinutes(5));
        po4.MarkApproved("GeneralManager", now.AddHours(-7));

        db.PurchaseOrders.AddRange(po1, po2, po3, po4, po5);
        await db.SaveChangesAsync(ct);
    }

    private static async Task CreateSchemaAsync(AppDbContext db, CancellationToken ct)
    {
        // Make sure the connection is open (SQLite creates the file when opened)
        await db.Database.OpenConnectionAsync(ct);

        var sql = @"
        CREATE TABLE IF NOT EXISTS PurchaseOrders (
            Id              TEXT    NOT NULL PRIMARY KEY,
            Amount          INTEGER NOT NULL,
            Price           TEXT    NOT NULL,
            Name            TEXT    NOT NULL,
            Status          TEXT    NOT NULL,
            ApprovedBy      TEXT    NULL,
            CreatedAtUtc    TEXT    NOT NULL,
            ApprovedAtUtc   TEXT    NULL,
            RejectedAtUtc   TEXT    NULL,
            RejectionReason TEXT    NULL
        );

        CREATE INDEX IF NOT EXISTS IX_PurchaseOrders_Status
        ON PurchaseOrders(Status);

        CREATE INDEX IF NOT EXISTS IX_PurchaseOrders_CreatedAtUtc
        ON PurchaseOrders(CreatedAtUtc);
        ";

        await db.Database.ExecuteSqlRawAsync(sql, ct);
    }
}


