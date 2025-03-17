using System;

namespace SampleProject.Application.Users.GetUser.Dto.Responses;

public record struct GetUserResponse
{
    public required string Username { get; init; }

    public required string Email { get; init; }
}