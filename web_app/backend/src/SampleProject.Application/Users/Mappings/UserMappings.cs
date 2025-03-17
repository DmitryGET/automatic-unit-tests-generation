using System;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Users.GetUser.Dto.Responses;
using SampleProject.Application.Users.UpdateUser.Dto.Responses;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.Mappings;

public static class UserMappings
{
    public static GetUserResponse ToGetUserResponse(this User user)
    {
        return new GetUserResponse
        {
            Username = user.Username,
            Email = user.Email
        };
    }

    public static UpdateUserResponse ToUpdateUserResponse(this IdentityUser user)
    {
        return new UpdateUserResponse
        {
            UserId = Guid.Parse(user.Id),
            Username = user.UserName,
            Email = user.Email
        };
    }
}