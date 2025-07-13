using System.Net;
using System.Net.Http.Json;
using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.Test.TestSetup;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace InvestorCommitments.Test.Controllers;

public class InvestorControllerTests : IClassFixture<InvestorCommitmentsWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly TestDbHelper _dbHelper;

    public InvestorControllerTests(InvestorCommitmentsWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        _dbHelper = factory.Services.GetRequiredService<TestDbHelper>();
        
        _dbHelper.CleanUpTables();
        _dbHelper.InitDbTables();
    }
    
    [Fact]
    public async Task GetInvestors_Returns_InvestorsWithTotalCommitments()
    {
        // Arrange
        var investor1Id = _dbHelper.AddInvestor("Alpha Fund", "PE", "UK");
        var investor2Id = _dbHelper.AddInvestor("Beta Capital", "VC", "US");
        _dbHelper.AddCommitment(investor2Id, "Stock", 100000, "GBP");
        _dbHelper.AddCommitment(investor2Id, "Infrastructure", 999, "GBP");
        
        // Act
        var response = await _client.GetAsync("/api/investors");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
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
    
    [Fact]
    public async Task GetInvestors_ReturnsEmpty_WhenNoInvestorsFound()
    {
        // Act
        var response = await _client.GetAsync("/api/investors");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var body = await response.Content.ReadFromJsonAsync<GetInvestorsResponse>();
        body.ShouldBeEquivalentTo(new GetInvestorsResponse
        {
            Investors = new List<Investor>()
        });
    }

    [Fact]
    public async Task GetInvestorCommitments_Returns_ListOfCommitmentsForInvestor()
    {
        // Arrange
        var investorId = _dbHelper.AddInvestor("Beta Capital", "VC", "US");
        _dbHelper.AddCommitment(investorId, "Real Estate", 100000, "GBP");
        _dbHelper.AddCommitment(investorId, "Hedge Fund", 1999, "GBP");
        _dbHelper.AddCommitment(investorId, "Infrastructure", 1234567, "GBP");
        _dbHelper.AddCommitment(investorId, "Natural Sources", 556654, "GBP");
        
        // Act
        var response = await _client.GetAsync($"/api/investor/{investorId}");
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var body = await response.Content.ReadFromJsonAsync<GetInvestorCommitmentsResponse>();
        
        
        body.ShouldBeEquivalentTo(new GetInvestorCommitmentsResponse
        {
            InvestorName = "Beta Capital",
            InvestorCommitments = new List<InvestorCommitment>
            {
                new InvestorCommitment
                {
                    AssetClass = "Real Estate",
                    Amount = 100000,
                    Currency = "GBP"
                },
                new InvestorCommitment
                {
                    AssetClass = "Hedge Fund",
                    Amount = 1999,
                    Currency =  "GBP"
                },
                new InvestorCommitment
                {
                    AssetClass = "Infrastructure",
                    Amount = 1234567,
                    Currency =  "GBP"
                },
                new InvestorCommitment
                {
                    AssetClass = "Natural Sources",
                    Amount = 556654,
                    Currency =  "GBP"
                },
            }
        });
    }
}