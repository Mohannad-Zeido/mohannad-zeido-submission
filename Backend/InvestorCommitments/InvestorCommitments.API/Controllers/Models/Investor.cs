using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record Investor
{
    [JsonPropertyName("id")]
    public required int Id { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("investory_type")]
    public required string InvestoryType { get; init; }
    [JsonPropertyName("country")]
    public required string Country { get; init; }
    [JsonPropertyName("total_commitments")]
    public required double TotalCommitments { get; init; }
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
}