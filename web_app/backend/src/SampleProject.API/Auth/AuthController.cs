using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Auths.Login;
using SampleProject.Application.Auths.Login.Dto.Responses;
using LoginRequest = SampleProject.Application.Auths.Login.Dto.Requests.LoginRequest;

namespace SampleProject.API.Auth;

[Route("api/v1/auth")]
[ApiVersion("1.0")]
[AllowAnonymous]
[ApiController]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Login User
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType<LoginResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _mediator.Send(new LoginCommand(request.Username, request.Password));
        return Ok(response);
    }
}