namespace InvestorCommitments.Infrastructure.Database;

public record DatabaseOptions
{
    public required string FilePath { get; init; } 
    public string ConnectionString => $"Data Source={FilePath};";
};