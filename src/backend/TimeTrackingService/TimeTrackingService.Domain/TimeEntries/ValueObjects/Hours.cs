using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.TimeEntries.ValueObjects;

public sealed record Hours
{
    private const decimal HalfHour = 0.5m;

    private Hours(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Result<Hours, Error> Create(decimal value)
    {
        if (value <= 0)
        {
            return Error.Validation(
                "time-entry.hours.positive.required",
                "Hours must be greater than zero.",
                nameof(value));
        }

        if (value > 24)
        {
            return Error.Validation(
                "time-entry.hours.maximum.exceeded",
                "Hours cannot be greater than 24 for one time entry.",
                nameof(value));
        }

        if (value % HalfHour != 0)
        {
            return Error.Validation(
                "time-entry.hours.step.invalid",
                "Hours must be a multiple of 0.5.",
                nameof(value));
        }

        return Result.Success<Hours, Error>(new Hours(value));
    }
}
