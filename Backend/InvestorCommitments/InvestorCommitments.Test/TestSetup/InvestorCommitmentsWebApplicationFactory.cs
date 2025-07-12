using InvestorCommitments.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorCommitments.Test.TestSetup;

public class InvestorCommitmentsWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;
    private TestDbHelper? DbHelper { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddSingleton<IDbConnectionFactory>(new TestSqliteConnectionFactory(_connection));

            DbHelper = new TestDbHelper(_connection);
            DbHelper.InitDb();
        });
    }
    
    public TestDbHelper GetDbHelper() => 
        DbHelper ?? throw new InvalidOperationException("DbHelper accessed before initialization.");

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Close();
            _connection?.Dispose();
        }
        base.Dispose(disposing);
    }
}