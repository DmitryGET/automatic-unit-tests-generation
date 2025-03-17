using System;
using SampleProject.Application.Exceptions.Base;

namespace SampleProject.Application.Exceptions;

public class EntityNotFoundException : BaseCustomException
{
    public EntityNotFoundException() : base("Entity not found")
    {
    }
    
    public EntityNotFoundException(string message) : base(message)
    {
    }
}