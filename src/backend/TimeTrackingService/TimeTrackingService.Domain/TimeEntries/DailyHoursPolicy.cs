using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain.TimeEntries.ValueObjects;

namespace TimeTrackingService.Domain.TimeEntries;

public static class DailyHoursPolicy
{
    public static Result<decimal, Error> ValidateAddition(
        decimal existingHours,
        Hours addedHours)
    {
        var totalHours = existingHours + addedHours.Value;
        if (totalHours > TimeEntryConstants.MAX_DAILY_HOURS)
        {
            return Error.Validation(
                "time-entry.daily-hours.maximum.exceeded",
                $"An employee cannot log more than {TimeEntryConstants.MAX_DAILY_HOURS} hours in one calendar day.");
        }

        return Result.Success<decimal, Error>(totalHours);
    }

    public static bool IsOvertime(decimal totalHours) =>
        totalHours > TimeEntryConstants.OVERTIME_HOURS_THRESHOLD;
}
