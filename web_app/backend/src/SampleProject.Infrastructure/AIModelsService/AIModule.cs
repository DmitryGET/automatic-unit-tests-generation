using Autofac;
using SampleProject.Application.AIModels.Interfaces;
using SampleProject.Infrastructure.AIModelsService.Models;

namespace SampleProject.Infrastructure.AIModelsService;

public class AIModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LlamaModelService>()
            .As<IAiModelService>()
            .InstancePerLifetimeScope();
    }
}