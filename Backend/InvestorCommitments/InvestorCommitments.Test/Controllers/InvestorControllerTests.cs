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
        var investor1Id = await _dbHelper.AddInvestor("Alpha Fund", "PE", "UK");
        var investor2Id = await _dbHelper.AddInvestor("Beta Capital", "VC", "US");
        await _dbHelper.AddCommitment(investor2Id, "Stock", 100000, "GBP");
        await _dbHelper.AddCommitment(investor2Id, "Infrastructure", 999, "GBP");
        
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
                    Id = investor1Id,
                    Name = "Alpha Fund",
                    InvestoryType = "PE",
                    Country = "UK",
                    TotalCommitments = 0,
                    Currency = "GBP"
                },
                new Investor
                {
                    Id = investor2Id,
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