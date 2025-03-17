using System;

namespace SampleProject.Application.Users.UpdateUser.Dto.Responses;

public record struct UpdateUserResponse
{
    public required Guid UserId { get; init; }

    public required string Username { get; init; }

    public required string Email { get; init; }
}