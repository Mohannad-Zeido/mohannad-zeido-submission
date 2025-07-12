namespace InvestorCommitments.Infrastructure.Database;

public record DatabaseOptions
{
    public required string FilePath { get; set; } 
    public string ConnectionString => $"Data Source={FilePath};";
};