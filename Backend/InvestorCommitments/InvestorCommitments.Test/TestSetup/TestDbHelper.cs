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
                                investor_name TEXT NOT NULL,
                                investory_type TEXT,
                                investor_country TEXT,
                                investor_date_added TEXT,
                                investor_last_updated TEXT
                            );

                            CREATE TABLE commitments (
                                id INTEGER PRIMARY KEY AUTOINCREMENT,
                                investor_id INTEGER,
                                commitment_asset_class TEXT,
                                commitment_amount REAL,
                                commitment_currency TEXT,
                                FOREIGN KEY (investor_id) REFERENCES investors(id)
                            );
                            """);
    }
    
    public async Task<int> AddInvestor(string investorName, string investoryType, string investorCountry)
    {
        return await _connection.ExecuteScalarAsync<int>("""
                                     INSERT INTO investors (
                                            investor_name, investory_type, investor_country, 
                                            investor_date_added, investor_last_updated
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
                                investor_id, commitment_asset_class, commitment_amount, commitment_currency
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