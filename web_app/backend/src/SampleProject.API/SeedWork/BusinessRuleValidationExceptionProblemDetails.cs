using Microsoft.AspNetCore.Http;
using SampleProject.Domain.SeedWork;

namespace SampleProject.API.SeedWork
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = exception.Details;
        }
    }
}