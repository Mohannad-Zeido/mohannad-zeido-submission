using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record Investor
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("investory_type")]
    public required string InvestoryType { get; set; }
    [JsonPropertyName("country")]
    public required string Country { get; set; }
    [JsonPropertyName("total_commitments")]
    public required double TotalCommitments { get; set; }
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }
}