using SampleProject.Domain.SeedWork;

namespace SampleProject.Application.Configuration.Rule;

public class RuleChecker : IRuleChecker
{
    public void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}