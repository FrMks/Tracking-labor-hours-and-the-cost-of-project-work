namespace TimeTrackingService.Domain.TimeEntries;

public readonly struct TimeEntryConstants
{
    public const decimal MAX_DAILY_HOURS = 24m;
    public const decimal OVERTIME_HOURS_THRESHOLD = 12m;
}
