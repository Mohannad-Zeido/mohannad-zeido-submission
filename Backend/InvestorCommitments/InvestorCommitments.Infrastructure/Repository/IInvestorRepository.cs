using InvestorCommitments.Infrastructure.Repository.Models;

namespace InvestorCommitments.Infrastructure.Repository;

public interface IInvestorRepository
{
    Task<IEnumerable<InvestorDto>> GetAllInvestorsAsync();
    Task<IEnumerable<InvestorCommitmentDto>> GetInvestorCommitmentsAsync(int investorId);
    public Task<InvestorDto?> GetInvestor(int investorId);
}