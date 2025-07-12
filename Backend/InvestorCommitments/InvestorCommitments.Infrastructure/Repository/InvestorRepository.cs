using InvestorCommitments.Infrastructure.Database;
using Dapper;
using InvestorCommitments.Infrastructure.Repository.Model;

namespace InvestorCommitments.Infrastructure.Repository;

public class InvestorRepository : IInvestorRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InvestorRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    
    public async Task<IEnumerable<Investor>> GetAllInvestorsAsync()
    {
        const string sql =
            """
            SELECT 
                i.*, 
                COALESCE(totalCommitmentSum, 0) AS totalCommitment
            FROM 
                investors i
            LEFT JOIN (
                SELECT 
                    investorId, 
                    SUM(amount) AS totalCommitmentSum
                FROM 
                    commitments
                GROUP BY 
                    investorId
            ) calculatedCommitments ON i.id = calculatedCommitments.investorId;
            """;
        
        using var connection = _connectionFactory.CreateConnection();
        return await connection.QueryAsync<Investor>(sql);
    }
}