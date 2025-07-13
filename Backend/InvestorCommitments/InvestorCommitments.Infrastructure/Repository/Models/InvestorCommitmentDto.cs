namespace InvestorCommitments.Infrastructure.Repository.Models;

public record InvestorCommitmentDto
{
    public required int Id { get; init; }
    public required string AssetClass {get; init;}
    public required double Amount {get; init;}
    public required string Currency {get; init;}
}