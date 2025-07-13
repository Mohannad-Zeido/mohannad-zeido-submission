using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.Infrastructure.Repository.Models;

namespace InvestorCommitments.API.Extensions;

public static class InvestorCommitmentDtoExtensions
{
    public static InvestorCommitment ToInvestorCommitment(this InvestorCommitmentDto investorCommitmentDto)
    {
        return new InvestorCommitment
        {
            Id = investorCommitmentDto.Id,
            Amount = investorCommitmentDto.Amount,
            AssetClass = investorCommitmentDto.AssetClass,
            Currency = investorCommitmentDto.Currency,
        };
    }
}