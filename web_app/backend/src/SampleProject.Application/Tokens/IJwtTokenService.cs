using System;
using Microsoft.AspNetCore.Identity;

namespace SampleProject.Application.Tokens;

public interface IJwtTokenService
{
    string CreateToken(IdentityUser user);
    
    Guid GetCurrentUserIdFromJwtToken();
}