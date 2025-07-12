using Dapper;
using Microsoft.Data.Sqlite;

namespace InvestorCommitments.Test.TestSetup;

public class TestDbHelper
{
    private readonly SqliteConnection _connection;

    public TestDbHelper(SqliteConnection connection)
    {
        _connection = connection;
    }
    public void InitDb()
    {
        _connection.Execute("""
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
    
    public async Task<int> AddInvestor(string investorName, string investoryType, string investorCountry)
    {
        return await _connection.ExecuteScalarAsync<int>("""
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
    
    public async Task AddCommitment(int investorId, string assetClass, double amount, string currency)
    {
        await _connection.ExecuteAsync("""
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
}