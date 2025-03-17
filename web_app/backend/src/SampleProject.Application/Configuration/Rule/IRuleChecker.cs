using SampleProject.Domain.SeedWork;

namespace SampleProject.Application.Configuration.Rule;

public interface IRuleChecker
{
    void CheckRule(IBusinessRule rule);
}