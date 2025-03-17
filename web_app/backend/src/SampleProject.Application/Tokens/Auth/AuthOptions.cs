using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SampleProject.Infrastructure.Auth;

public class AuthOptions
{
    private const string Key = "mysupersecret_secretkey!123#%$^&$";
    
    public const string Issuer = "TestGenServer";
    public const string Audience = "TestGenClient";
    public const int Lifetime = 60;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
    }
}