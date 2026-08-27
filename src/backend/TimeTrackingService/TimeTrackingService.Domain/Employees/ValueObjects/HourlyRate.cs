using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.Employees.ValueObjects;

public sealed record HourlyRate
{
    private HourlyRate(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static Result<HourlyRate, Error> Create(decimal value)
    {
        if (value <= 0)
        {
            return Error.Validation(
                "employee.hourly-rate.positive.required",
                "Hourly rate must be greater than zero.");
        }

        if (decimal.Round(value, 2) != value)
        {
            return Error.Validation(
                "employee.hourly-rate.precision.invalid",
                "Hourly rate cannot contain more than two decimal places.");
        }

        return Result.Success<HourlyRate, Error>(new HourlyRate(value));
    }
}
