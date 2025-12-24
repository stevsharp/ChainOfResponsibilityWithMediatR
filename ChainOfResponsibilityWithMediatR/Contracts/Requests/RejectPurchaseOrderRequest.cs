namespace ChainOfResponsibility.Api.Contracts.Requests;

public sealed record RejectPurchaseOrderRequest(
    string Reason
);