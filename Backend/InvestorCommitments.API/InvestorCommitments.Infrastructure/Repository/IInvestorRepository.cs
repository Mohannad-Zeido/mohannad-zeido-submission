namespace InvestorCommitments.Infrastructure.Repository;

public interface IInvestorRepository
{
    Task<IEnumerable<string>> GetAllInvestorNamesAsync();
}