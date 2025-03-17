using SampleProject.Application.Exceptions.Base;

namespace SampleProject.Application.Exceptions;

public class ForbiddenException : BaseCustomException
{
    public ForbiddenException() : base("Action forbidden")
    {
    }
    
    public ForbiddenException(string message) : base(message)
    {
    }
}