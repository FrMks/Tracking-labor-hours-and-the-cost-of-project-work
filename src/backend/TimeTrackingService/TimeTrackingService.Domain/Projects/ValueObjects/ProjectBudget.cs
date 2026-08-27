using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.Projects.ValueObjects;

public sealed record ProjectBudget
{
    private ProjectBudget(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Result<ProjectBudget, Error> Create(decimal value)
    {
        if (value < 0)
        {
            return Error.Validation(
                "project.budget.negative",
                "Project budget cannot be negative.",
                nameof(value));
        }

        if (decimal.Round(value, 2) != value)
        {
            return Error.Validation(
                "project.budget.precision.invalid",
                "Project budget cannot contain more than two decimal places.",
                nameof(value));
        }

        return Result.Success<ProjectBudget, Error>(new ProjectBudget(value));
    }
}
