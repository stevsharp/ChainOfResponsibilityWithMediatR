namespace ChainOfResponsibility.Api.Contracts.Responses;

public sealed record ApprovalDecisionDto(bool Approved, string ApprovedBy);
