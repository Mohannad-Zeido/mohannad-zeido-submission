using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.Infrastructure.Repository.Models;

namespace InvestorCommitments.API.Extensions;

public static class InvestorDtoExtensions
{
    public static Investor ToInvestor(this InvestorDto investorDto)
    {
        return new Investor
        {
            Id = investorDto.Id,
            Name = investorDto.Name,
            InvestoryType = investorDto.InvestoryType,
            Country = investorDto.Country,
            Currency = "GBP",
            TotalCommitments = investorDto.TotalCommitments ?? 0,
        };
    }
}