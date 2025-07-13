using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record GetInvestorCommitmentsResponse
{
    [JsonPropertyName("investor_Name")]
    public required string InvestorName {get; init;}
    [JsonPropertyName("investor_commitments")]
    public required IEnumerable<InvestorCommitment> InvestorCommitments {get; init;}
}