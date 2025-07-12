using System.Data;

namespace InvestorCommitments.Infrastructure.Database;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}