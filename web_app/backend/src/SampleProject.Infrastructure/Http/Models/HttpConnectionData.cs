using System;

namespace SampleProject.Infrastructure.Http.Models;

public readonly struct HttpConnectionData
{
    public string? ClientName { get; init; }
    
    public TimeSpan? TimeOut { get; init; }
}
