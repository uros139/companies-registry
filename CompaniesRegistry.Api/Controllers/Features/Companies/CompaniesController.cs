using CompaniesRegistry.Api.Extensions;
using CompaniesRegistry.Application.Features.Companies.Create;
using CompaniesRegistry.Application.Features.Companies.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Controllers.Features.Companies;

[Route("api/[controller]")]
public class CompaniesController(IMediator mediator) : Controller
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompanyResponse>>> Get(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCompaniesQuery(), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Post([FromBody] CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        var companyId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(Post), new { id = companyId }, companyId);
    }
}
