using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.ClosedPeriods;

public sealed class ClosedPeriod
{
    private ClosedPeriod(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; private set; }

    public int Month { get; private set; }

    public bool Contains(DateOnly date) => date.Year == Year && date.Month == Month;

    public static Result<ClosedPeriod, Error> Create(int year, int month)
    {
        if (year is < 1 or > 9999)
        {
            return Error.Validation(
                "closed-period.year.invalid",
                "Year must be between 1 and 9999.",
                nameof(year));
        }

        if (month is < 1 or > 12)
        {
            return Error.Validation(
                "closed-period.month.invalid",
                "Month must be between 1 and 12.",
                nameof(month));
        }

        return Result.Success<ClosedPeriod, Error>(new ClosedPeriod(year, month));
    }
}
