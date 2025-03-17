using SampleProject.Application.Exceptions.Base;

namespace SampleProject.Application.Exceptions;

public class NonUniqueUsernameException : BaseCustomException
{
    public NonUniqueUsernameException() : base("Username already exists")
    {
    }
    
    public NonUniqueUsernameException(string message) : base(message)
    {
    }
}