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
    public async Task<IActionResult> Get()
    {
        var response = await _investorRepository.GetAllInvestorNamesAsync();
        
        return Ok(response);
    }
    
}