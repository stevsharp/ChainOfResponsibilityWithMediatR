````md
# ChainOfResponsibilityWithMediatR

Small demo project that shows how to use **Chain of Responsibility** and **Specification** inside a **CQRS** flow with **MediatR**, using **Carter** endpoints and **SQLite**.

My goal was to keep it practical and easy to follow, not “too enterprise”, but still clean.

## What this project demonstrates

- CQRS structure (Commands for write, Queries for read)
- MediatR handlers, one handler per request
- Pipeline behaviors (logging, validation, transaction for commands)
- Domain approval workflow using Chain of Responsibility (Employee, Supervisor, General Manager)
- Specification pattern to keep approval rules readable
- Carter for endpoints
- SQLite storage
- Demo seed data, creates schema without EF migrations

## Tech stack

- .NET (ASP.NET Core)
- Carter (endpoint modules)
- MediatR (CQRS)
- FluentValidation (validators)
- EF Core + SQLite

## How Chain of Responsibility is used here

In the domain, we have an `ApprovalChain` that runs multiple approvers in order:

1. Employee approver
2. Supervisor approver
3. General manager approver (fallback)

Each approver decides if it can approve the Purchase Order. If not, it returns `null` and the next approver tries.

The decision looks like:

- `Approved = true`
- `ApprovedBy = "Employee" | "Supervisor" | "GeneralManager"`

Rules are expressed with Specifications, for example:

- Employee approves when `Price < 100` and `Amount == 1`
- Supervisor approves when `Price < 200` and `Amount <= 2`
- General manager approves anything that reaches the end

## API Endpoints

Base path: `/api/purchase-orders`

- `POST /`  
  Create a purchase order

- `GET /{id}`  
  Get purchase order by id

- `GET /?page=1&pageSize=20`  
  List purchase orders (paged, page and pageSize are optional if you set defaults)

- `POST /{id}/approve`  
  Approve purchase order (runs approval chain)

- `POST /{id}/reject`  
  Reject purchase order

Swagger UI:
- `/swagger`

## Request examples

### Create
```http
POST /api/purchase-orders
Content-Type: application/json

{
  "amount": 1,
  "price": 50,
  "name": "Test Room 1"
}
````

### List

```http
GET /api/purchase-orders?page=1&pageSize=20
```

### Approve

```http
POST /api/purchase-orders/{id}/approve
```

### Reject

```http
POST /api/purchase-orders/{id}/reject
Content-Type: application/json

{
  "reason": "Budget not approved"
}
```

## Project structure (high level)

```
src/
  ChainOfResponsibility.Api/
    Endpoints/ (Carter modules)
    Contracts/ (Requests, Responses)

  ChainOfResponsibility.Application/
    Abstractions/ (ICommand, IQuery, repositories, clock)
    PurchaseOrders/
      Commands/
      Queries/
    Common/Behaviors/ (MediatR pipeline behaviors)

  ChainOfResponsibility.Domain/
    PurchaseOrders/ (entity + status)
    Approvals/ (ApprovalChain, approvers, decision)
    Specifications/ (Specification pattern)

  ChainOfResponsibility.Infrastructure/
    Persistence/ (DbContext, configs, repository, DbSeeder)
    Time/ (SystemClockService)
```

## Running the project

### 1) Restore and run

From the solution root:

```bash
dotnet restore
dotnet run --project src/ChainOfResponsibility.Api
```

Then open:

* `https://localhost:xxxx/swagger`

### 2) Database

This project uses SQLite, connection string example:

```json
"ConnectionStrings": {
  "Default": "Data Source=app.db"
}
```

The schema is created by `DbSeeder` using raw SQL (no migrations).
When running in Development, the seed will:

1. Create table `PurchaseOrders` if missing
2. Insert demo data if the table is empty

## Seeding (no migrations)

Seeder creates schema:

* `PurchaseOrders` table
* indexes on `Status` and `CreatedAtUtc`

If you want to disable seeding, remove the `DbSeeder.SeedAsync(db)` call from `Program.cs`.

## Pipeline behaviors

* LoggingBehavior (logs request name and execution time)
* ValidationBehavior (runs FluentValidation validators)
* TransactionBehavior (wraps only commands, using `ICommand` markers)

Queries are not wrapped in transaction behavior.

## Troubleshooting

### Swagger throws: “Required parameter page was not provided”

Make `page` and `pageSize` optional in the Carter endpoint:

* use `int? page, int? pageSize`, then set defaults inside the handler
* or use `int page = 1, int pageSize = 20`

### SQLite: “no such table: PurchaseOrders”

That means schema was not created yet.

* Ensure `DbSeeder.SeedAsync(...)` is called on startup (Development)
* Ensure the connection string points to the same `app.db` you are checking
* Delete old `app.db` files if you have multiple (common with relative paths)

## Why this approach

I like CQRS because it keeps each feature small and focused:

* one request
* one handler
* easy to test

And I like Chain of Responsibility because approval workflows grow fast and become messy if you put them inside one big `if` block.

