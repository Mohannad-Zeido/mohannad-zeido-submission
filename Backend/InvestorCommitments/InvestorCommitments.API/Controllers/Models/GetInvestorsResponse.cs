using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record GetInvestorsResponse
{
    [JsonPropertyName("investors")]
    public required IEnumerable<Investor> Investors { get; init; }
};