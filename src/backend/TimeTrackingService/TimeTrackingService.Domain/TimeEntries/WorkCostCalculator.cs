using TimeTrackingService.Domain.Employees.ValueObjects;
using TimeTrackingService.Domain.TimeEntries.ValueObjects;

namespace TimeTrackingService.Domain.TimeEntries;

public static class WorkCostCalculator
{
    public static decimal Calculate(Hours hours, HourlyRate hourlyRate) =>
        decimal.Round(
            hours.Value * hourlyRate.Value,
            2,
            MidpointRounding.AwayFromZero);
}
