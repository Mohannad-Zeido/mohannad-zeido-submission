using System.Net.Http.Json;
using InvestorCommitments.API.Controllers.Models;
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
    public async Task GetInvestors_ReturnsInvestorsWithTotalCommitments()
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
        
        var body = await response.Content.ReadFromJsonAsync<GetInvestorsResponse>();
        
        
        body.ShouldBeEquivalentTo(new GetInvestorsResponse
        {
            Investors = new List<Investor>
            {
                new Investor
                {
                    Name = "Alpha Fund",
                    InvestoryType = "PE",
                    Country = "UK",
                    TotalCommitments = 0,
                    Currency = "GBP"
                },
                new Investor
                {
                  Name = "Beta Capital",
                  InvestoryType = "VC",
                  Country = "US",
                  TotalCommitments = 100999,
                  Currency = "GBP"
                },
            }
        });
    }
}