using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.API.Extenstions;
using InvestorCommitments.Infrastructure.Repository;
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
        var investorsList = await _investorRepository.GetAllInvestorsAsync();
        
        return Ok(new GetInvestorsResponse
        {
            Investors = investorsList.Select(i => i.ToInvestor())
        });
    }
    
}