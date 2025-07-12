namespace InvestorCommitments.Infrastructure.Repository.Model;

public record Investor
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string InvestoryType { get; set; }
    public required string Country { get; set; }
    public required int TotalCommitment { get; set; }
}