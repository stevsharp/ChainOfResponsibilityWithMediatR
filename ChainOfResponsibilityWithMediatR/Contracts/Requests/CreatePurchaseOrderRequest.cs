namespace ChainOfResponsibility.Api.Contracts.Requests;

public sealed record 
    CreatePurchaseOrderRequest(int Amount, decimal price, string Name);
