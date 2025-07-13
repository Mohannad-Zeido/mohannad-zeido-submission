namespace InvestorCommitments.Infrastructure.Repository.Models;

public record InvestorDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string InvestoryType { get; set; }
    public required string Country { get; set; }
    public required double  TotalCommitments { get; set; }
}