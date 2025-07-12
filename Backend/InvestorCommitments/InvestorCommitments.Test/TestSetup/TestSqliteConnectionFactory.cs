using System.Data;
using InvestorCommitments.Infrastructure.Database;
using Microsoft.Data.Sqlite;

namespace InvestorCommitments.Test.TestSetup;

public class TestSqliteConnectionFactory : IDbConnectionFactory
{
    private readonly SqliteConnection _sharedConnection;

    public TestSqliteConnectionFactory(SqliteConnection connection)
    {
        _sharedConnection = connection;
    }

    public IDbConnection CreateConnection()
    {
        return _sharedConnection;
    }
}