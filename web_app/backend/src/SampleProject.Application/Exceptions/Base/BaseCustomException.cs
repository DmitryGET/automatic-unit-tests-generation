using System;

namespace SampleProject.Application.Exceptions.Base;

public abstract class BaseCustomException : Exception
{
    protected BaseCustomException()
    {
        
    }
    
    protected BaseCustomException(string message) : base(message)
    {
    }

    protected BaseCustomException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}