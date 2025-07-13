using Dapper;
using InvestorCommitments.Infrastructure.Database;

namespace InvestorCommitments.Test.TestSetup;

public class TestDbHelper
{
    private readonly IDbConnectionFactory _dbConnectionFactoryFactory;

    public TestDbHelper(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactoryFactory = dbConnectionFactory;
    }
    
    public void InitDbTables()
    {
        using var connection = _dbConnectionFactoryFactory.CreateConnection();
        connection.Execute("""
                           CREATE TABLE investors (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               name TEXT,
                               investoryType TEXT,
                               country TEXT,
                               dateAdded TEXT,
                               lastUpdated TEXT,
                               UNIQUE(name)
                           );

                           CREATE TABLE commitments (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               investorId INTEGER,
                               assetClass TEXT,
                               amount INTEGER,
                               currency TEXT,
                               FOREIGN KEY (investorId) REFERENCES investors(id)
                           );
                           """);
    }
    
    public int AddInvestor(string investorName, string investoryType, string investorCountry)
    {
        using var connection = _dbConnectionFactoryFactory.CreateConnection();
        return connection.ExecuteScalar<int>("""
                                      INSERT INTO investors (
                                             name, investoryType, country, 
                                             dateAdded, lastUpdated
                                             )
                                      VALUES (@Name, @Type, @Country, @DateAdded, @LastUpdated)
                                      RETURNING id
                                      """, new
        {
            Name = investorName,
            Type = investoryType,
            Country = investorCountry,
            DateAdded = DateTime.Now.ToString("yyyy-MM-dd"),
            LastUpdated = DateTime.Now.ToString("yyyy-MM-dd")
        });
    }
    
    public void AddCommitment(int investorId, string assetClass, double amount, string currency)
    {
        using var connection = _dbConnectionFactoryFactory.CreateConnection();
        connection.Execute("""
                           INSERT INTO commitments (
                               investorId, assetClass, amount, currency
                           ) VALUES (
                               @InvestorId, @AssetClass, @Amount, @Currency
                           )
                           """, new
        {
            InvestorId = investorId,
            AssetClass = assetClass,
            Amount = amount,
            Currency = currency
        });
    }

    public void CleanUpTables()
    { 
        using var connection = _dbConnectionFactoryFactory.CreateConnection();
        
       connection.ExecuteAsync("""
                           DROP TABLE IF EXISTS commitments;
                           DROP TABLE IF EXISTS  investors;
                           """);
    }
}