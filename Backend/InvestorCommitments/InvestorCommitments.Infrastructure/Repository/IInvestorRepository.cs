using InvestorCommitments.Infrastructure.Repository.Models;

namespace InvestorCommitments.Infrastructure.Repository;

public interface IInvestorRepository
{
    Task<IEnumerable<InvestorDto>> GetAllInvestorsAsync();
}