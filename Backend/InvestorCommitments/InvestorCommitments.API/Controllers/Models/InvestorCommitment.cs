using System.Text.Json.Serialization;

namespace InvestorCommitments.API.Controllers.Models;

public record InvestorCommitment
{
    [JsonPropertyName("id")]
    public required int Id {get; init;}
    [JsonPropertyName("asset_class")]
    public required string AssetClass {get; init;}
    [JsonPropertyName("amount")]
    public required double Amount {get; init;}
    [JsonPropertyName("currency")]
    public required string Currency {get; init;}
    
}