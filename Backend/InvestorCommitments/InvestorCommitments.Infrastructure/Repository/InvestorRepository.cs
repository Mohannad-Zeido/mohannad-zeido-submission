using InvestorCommitments.Infrastructure.Database;
using Dapper;
using InvestorCommitments.Infrastructure.Repository.Models;

namespace InvestorCommitments.Infrastructure.Repository;

public class InvestorRepository : IInvestorRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InvestorRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    
    public async Task<IEnumerable<InvestorDto>> GetAllInvestorsAsync()
    {
        const string sql =
            """
            SELECT 
                i.*, 
                COALESCE(totalCommitmentsSum, 0) AS totalCommitments
            FROM 
                investors i
            LEFT JOIN (
                SELECT 
                    investorId, 
                    SUM(amount) AS totalCommitmentsSum
                FROM 
                    commitments
                GROUP BY 
                    investorId
            ) calculatedCommitments ON i.id = calculatedCommitments.investorId;
            """;
        
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<InvestorDto>(sql);
    }
    
    public async Task<InvestorDto?> GetInvestor(int investorId)
    {
        const string sql =
            @"
            SELECT *
            FROM investors
            WHERE id = @InvestorId;
            ";
        
        using var connection = _connectionFactory.CreateConnection();
        var investor = await connection.QueryFirstOrDefaultAsync<InvestorDto>(sql, new { InvestorId = investorId });
        return investor;
    }

    public async Task<IEnumerable<InvestorCommitmentDto>> GetInvestorCommitmentsAsync(int investorId)
    {
        const string sql = @"
        SELECT *
        FROM commitments
        WHERE investorId = @InvestorId;
    ";

        using var connection = _connectionFactory.CreateConnection();
        var commitments = await connection.QueryAsync<InvestorCommitmentDto>(sql, new { InvestorId = investorId });
        return commitments;
    }
}