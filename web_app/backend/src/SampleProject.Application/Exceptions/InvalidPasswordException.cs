using System;
using SampleProject.Application.Exceptions.Base;

namespace SampleProject.Application.Exceptions;

public class InvalidPasswordException : BaseCustomException
{
    public InvalidPasswordException() : base("Invalid password")
    {
    }
    
    public InvalidPasswordException(string message) : base(message)
    {
    }
}