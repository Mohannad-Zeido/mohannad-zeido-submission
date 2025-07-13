namespace InvestorCommitments.Infrastructure.Repository.Models;

public record InvestorDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string InvestoryType { get; init; }
    public required string Country { get; init; }
    public double?  TotalCommitments { get; init; }
}