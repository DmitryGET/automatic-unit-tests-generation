using System.Reflection;
using SampleProject.Application.Auths.Login;

namespace SampleProject.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(LoginCommand).Assembly;
    }
}