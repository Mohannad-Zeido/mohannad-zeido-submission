using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record Investor
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("investory_type")]
    public required string InvestoryType { get; set; }
    [JsonPropertyName("country")]
    public required string Country { get; set; }
    [JsonPropertyName("total_commitments")]
    public required int TotalCommitments { get; set; }
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }
}