using InvestorCommitments.Infrastructure.Database;
using Dapper;

namespace InvestorCommitments.Infrastructure.Repository;

public class InvestorRepository : IInvestorRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public InvestorRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    
    public async Task<IEnumerable<string>> GetAllInvestorNamesAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT DISTINCT investor_name FROM investors";
        var results = await connection.QueryAsync<string>(sql);
        return results;
    }
}