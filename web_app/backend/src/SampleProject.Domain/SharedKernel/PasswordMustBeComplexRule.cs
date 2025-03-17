using System.Text.RegularExpressions;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class PasswordMustBeComplexRule : IBusinessRule
{
    private readonly string _password;

    public PasswordMustBeComplexRule(string password)
    {
        _password = password;
    }

    public bool IsBroken()
    {
        return _password.Length < 6 
               || !Regex.IsMatch(_password, @"[A-Z]") 
               || !Regex.IsMatch(_password, @"[a-z]") 
               || !Regex.IsMatch(_password, @"[0-9]")
               || !Regex.IsMatch(_password, @"[\W]");
    }

    public string Message =>
        "Password must have at least one non alphanumeric character, one digit ('0'-'9'), one uppercase ('A'-'Z'). " +
        "Password must be at least 6 characters.";
}