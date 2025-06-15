using CompaniesRegistry.Application.Features.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Controllers.Users;

[Route("api/[controller]")]
public class UsersController(IMediator mediator) : Controller
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var userId = await mediator.Send(command, cancellationToken);
        return StatusCode(StatusCodes.Status201Created, userId);
    }
}
