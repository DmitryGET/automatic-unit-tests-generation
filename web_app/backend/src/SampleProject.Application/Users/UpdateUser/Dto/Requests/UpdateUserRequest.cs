using System;
using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;

namespace SampleProject.Application.Users.UpdateUser.Dto.Requests;

public record struct UpdateUserRequest
{
    public required string Username { get; init; }

    [LogMasked]
    public required string Password { get; init; }

    public required string Email { get; init; }
}