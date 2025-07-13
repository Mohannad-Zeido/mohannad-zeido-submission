using InvestorCommitments.API.Controllers.Models;
using InvestorCommitments.API.Extensions;
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
    
    [HttpGet]
    [Route("investor/{investorId:int}")]
    public async Task<ActionResult<GetInvestorsResponse>> GetInvestorCommitments(int investorId)
    {
        var investor = await _investorRepository.GetInvestor(investorId);

        if (investor == null)
        {
            return NotFound();
        }
        
        var investorsList = await _investorRepository.GetInvestorCommitmentsAsync(investorId);
        
        return Ok(new GetInvestorCommitmentsResponse
        {
            InvestorName = investor.Name,
            InvestorCommitments = investorsList.Select(ic => ic.ToInvestorCommitment())
        });
    }
    
}