using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain.ClosedPeriods;
using TimeTrackingService.Domain.Employees;
using TimeTrackingService.Domain.Projects;
using TimeTrackingService.Domain.TimeEntries.ValueObjects;

namespace TimeTrackingService.Domain.TimeEntries;

public static class TimeEntryCreationPolicy
{
    public static Result<TimeEntryCreationResult, Error> Validate(
        Employee employee,
        Project project,
        ClosedPeriod? closedPeriod,
        DateOnly entryDate,
        Hours hours,
        decimal existingDailyHours)
    {
        if (closedPeriod?.Contains(entryDate) == true)
        {
            return Error.Conflict(
                "time-entry.period.closed",
                "Time entries cannot be created in a closed period.");
        }

        if (!project.Period.Contains(entryDate))
        {
            return Error.Validation(
                "time-entry.project-period.invalid",
                "Time entry date must be within the project period.",
                nameof(entryDate));
        }

        var hourlyRateResult = employee.GetHourlyRateOn(entryDate);
        if (hourlyRateResult.IsFailure)
        {
            return hourlyRateResult.Error;
        }

        var dailyHoursResult = DailyHoursPolicy.ValidateAddition(existingDailyHours, hours);
        if (dailyHoursResult.IsFailure)
        {
            return dailyHoursResult.Error;
        }

        var totalDailyHours = dailyHoursResult.Value;
        var appliedHourlyRate = hourlyRateResult.Value;

        return Result.Success<TimeEntryCreationResult, Error>(new TimeEntryCreationResult(
            appliedHourlyRate,
            WorkCostCalculator.Calculate(hours, appliedHourlyRate),
            DailyHoursPolicy.IsOvertime(totalDailyHours)));
    }
}
