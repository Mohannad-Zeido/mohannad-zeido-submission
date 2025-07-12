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
}