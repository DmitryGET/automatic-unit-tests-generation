using Autofac;
using SampleProject.Application.Tokens;

namespace SampleProject.Infrastructure.Security;

public class SecurityModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JwtTokenService>()
            .As<IJwtTokenService>()
            .InstancePerLifetimeScope();
    }
}