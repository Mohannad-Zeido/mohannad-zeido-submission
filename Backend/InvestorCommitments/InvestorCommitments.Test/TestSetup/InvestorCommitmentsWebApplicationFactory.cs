using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorCommitments.Test.TestSetup;

public class InvestorCommitmentsWebApplicationFactory : WebApplicationFactory<Program>, IDisposable
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var configLocation = Path.Combine(Directory.GetCurrentDirectory(), "testsettings.json");
            configurationBuilder.AddJsonFile(configLocation);
        });
        
        builder.ConfigureServices(services =>
        {
            services.AddSingleton<TestDbHelper>();
        });
    }

    public new void Dispose()
    {
        Server.Dispose();
        base.Dispose();
    }
}