using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Controllers.Features.Companies;

[Route("api/[controller]")]
public class CompaniesController(IMediator mediator) : Controller
{

}
