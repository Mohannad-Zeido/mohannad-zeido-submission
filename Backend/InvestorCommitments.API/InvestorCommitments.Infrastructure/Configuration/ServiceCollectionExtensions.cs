using InvestorCommitments.Infrastructure.Database;
using InvestorCommitments.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace InvestorCommitments.Infrastructure.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseOptions"));
        
        services.AddSingleton<IDbConnectionFactory>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            return new SqliteConnectionFactory(options.ConnectionString);
        });
        
        services.AddScoped<IInvestorRepository, InvestorRepository>();
        
        return services;
    }
}