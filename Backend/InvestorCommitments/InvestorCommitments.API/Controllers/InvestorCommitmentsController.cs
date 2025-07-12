using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.API.Extenstions;
using InvestorCommitments.Infrastructure.Repository;
using InvestorCommitments.Infrastructure.Repository.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestorCommitments.API.Controllers;

[ApiController]
[Route("api/")]
public class InvestorCommitmentsController : ControllerBase
{
    private readonly IInvestorRepository _investorRepository;
    
    public InvestorCommitmentsController(IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    [HttpGet]
    [Route("investors")]
    public async Task<ActionResult<GetInvestorsResponse>> GetInvestors()
    {
        var investorDtos = await _investorRepository.GetAllInvestorsAsync();
        
        return Ok(new GetInvestorsResponse
        {
            Investors = investorDtos.Select(i => i.ToInvestor())
        });
    }
    
}