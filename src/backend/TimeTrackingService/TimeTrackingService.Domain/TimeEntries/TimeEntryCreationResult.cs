using TimeTrackingService.Domain.Employees.ValueObjects;

namespace TimeTrackingService.Domain.TimeEntries;

public sealed record TimeEntryCreationResult(
    HourlyRate AppliedHourlyRate,
    decimal Cost,
    bool IsOvertime);
