using CompaniesRegistry.Api.Extensions;
using CompaniesRegistry.Application.Features.Users.GetById;
using CompaniesRegistry.Application.Features.Users.Login;
using CompaniesRegistry.Application.Features.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Controllers.Users;

[Route("api/[controller]")]
public class UsersController(IMediator mediator) : Controller
{
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);
        return result.ToActionResult();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<string>> Login(
        [FromBody] LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var token = await mediator.Send(command, cancellationToken);
        return token.ToActionResult();
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Register(
        [FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await mediator.Send(command, cancellationToken);
        return user.ToCreatedActionResult(nameof(GetById), new { id = user.Id });
    }
}
