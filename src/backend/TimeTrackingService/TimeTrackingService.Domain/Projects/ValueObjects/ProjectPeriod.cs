using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.Projects.ValueObjects;

public sealed record ProjectPeriod
{
    private ProjectPeriod(DateOnly startDate, DateOnly? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; }

    public DateOnly? EndDate { get; }

    public static Result<ProjectPeriod, Error> Create(
        DateOnly startDate,
        DateOnly? endDate)
    {
        if (endDate.HasValue && endDate.Value < startDate)
        {
            return Error.Validation(
                "project.period.invalid",
                "Project end date cannot be earlier than its start date.",
                nameof(endDate));
        }

        return Result.Success<ProjectPeriod, Error>(new ProjectPeriod(startDate, endDate));
    }
}
