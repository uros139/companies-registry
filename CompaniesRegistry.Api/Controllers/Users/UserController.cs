using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Controllers.Users;

[Route("api/[controller]")]
public class UserController(IMediator mediator) : Controller
{
}
