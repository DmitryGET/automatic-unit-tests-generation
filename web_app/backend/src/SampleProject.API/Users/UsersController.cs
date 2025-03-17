using System;
using System.Net;
using System.Threading.Tasks;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Users.GetAllUsers;
using SampleProject.Application.Users.GetAllUsers.Dto.Responses;
using SampleProject.Application.Users.GetUser;
using SampleProject.Application.Users.GetUser.Dto.Responses;
using SampleProject.Application.Users.RegisterUser;
using SampleProject.Application.Users.RegisterUser.Dto.Requests;
using SampleProject.Application.Users.RegisterUser.Dto.Responses;
using SampleProject.Application.Users.RemoveUser;
using SampleProject.Application.Users.UpdateUser;
using SampleProject.Application.Users.UpdateUser.Dto.Requests;
using SampleProject.Application.Users.UpdateUser.Dto.Responses;
using SampleProject.Domain.Users;

namespace SampleProject.API.Users;

[Route("api/v1/users")]
[ApiVersion("1.0")]
[AllowAnonymous]
[ApiController]
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Register user
    /// </summary>
    [HttpPost]
    [ProducesResponseType<RegisterUserResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var response = await _mediator.Send(new RegisterUserCommand(request.Username, request.Password));
        return Ok(response);
    }

    /// <summary>
    /// Get user info
    /// </summary>
    [Authorize]
    [HttpGet("{userId:guid}")]
    [ProducesResponseType<GetUserResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        var response = await _mediator.Send(new GetUserCommand(userId));
        return Ok(response);
    }

    /// <summary>
    /// Update user info
    /// </summary>
    [Authorize]
    [HttpPut("{userId:guid}")]
    [ProducesResponseType<UpdateUserResponse>((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequest request)
    {
        var response = await _mediator.Send(new UpdateUserCommand(userId, request.Username, request.Password, request.Email));
        return Ok(response);
    }
}