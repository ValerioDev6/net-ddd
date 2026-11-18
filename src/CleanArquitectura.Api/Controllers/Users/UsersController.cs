using CleanArquitectura.Application.Users.RegisterUser;
using CleanArquitectura.Application.Users.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArquitectura.Domain.Abstraccions;
using CleanArquitectura.Domain.Users;
using CleanArquitectura.Application.Users.GetUsersPagination;

namespace CleanArquitectura.Api.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Nombre,
            request.Apellidos,
            request.Email,
            request.Password);
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Conflict(result.Error);
        }
        return Ok(result.Value);
    }


    [AllowAnonymous]
    [HttpGet("getPagination", Name = "PaginationUsers")]
    public async Task<ActionResult<PaginationResult<User, UserId>>> GetPagination(
       [FromQuery] GetUsersPaginationQuery request
   )
    {
        var resultados = await _sender.Send(request);
        return Ok(resultados);
    }
}
