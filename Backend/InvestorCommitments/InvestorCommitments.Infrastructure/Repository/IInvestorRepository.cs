using InvestorCommitments.Infrastructure.Repository.Model;

namespace InvestorCommitments.Infrastructure.Repository;

public interface IInvestorRepository
{
    Task<IEnumerable<Investor>> GetAllInvestorsAsync();
}