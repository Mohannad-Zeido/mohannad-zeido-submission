using System.Net.Http.Json;
using InvestorCommitments.Test.TestSetup;
using Shouldly;

namespace InvestorCommitments.Test.Controllers;

public class InvestorControllerTests : IClassFixture<InvestorCommitmentsWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly TestDbHelper _dbHelper;

    public InvestorControllerTests(InvestorCommitmentsWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _dbHelper = factory.GetDbHelper();
    }
    
    [Fact]
    public async Task GetInvestors_ReturnsInvestorsAndTotalCommitments()
    {
        // Arrange
        _ = await _dbHelper.AddInvestor("Alpha Fund", "PE", "UK");
        var investorToId = await _dbHelper.AddInvestor("Beta Capital", "VC", "US");
        await _dbHelper.AddCommitment(investorToId, "Stock", 100000, "GBP");
        await _dbHelper.AddCommitment(investorToId, "Infrastructure", 999, "GBP");
        
        // Act
        var response = await _client.GetAsync("/api/investors");
        
        // Assert
        response.IsSuccessStatusCode.ShouldBeTrue();
        
        var body = await response.Content.ReadFromJsonAsync<IEnumerable<object>>();
        
        
        body.ShouldBeEquivalentTo(new
        {
            Investors = new List<object>
            {
                new {
                    Name = "Alpha Fund",
                    InvestoryType = "PE",
                    InvestorCountry = "UK",
                    TotalCommitments = 0
                },
                new {
                  Name = "Beta Capital",
                  InvestoryType = "VC",
                  InvestorCountry = "US",
                  TotalCommitments = 100999
                },
            }
        });
    }
}