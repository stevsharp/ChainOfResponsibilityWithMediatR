namespace ChainOfResponsibility.Domain.Approvals;

public sealed record ApprovalDecision( bool Approved, string ApprovedBy);
