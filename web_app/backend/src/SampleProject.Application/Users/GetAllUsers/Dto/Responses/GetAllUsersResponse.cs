using System.Collections.Generic;
using SampleProject.Application.Users.GetUser.Dto.Responses;

namespace SampleProject.Application.Users.GetAllUsers.Dto.Responses;

public record struct GetAllUsersResponse
{
    public required IEnumerable<GetUserResponse> Users { get; init; }
}