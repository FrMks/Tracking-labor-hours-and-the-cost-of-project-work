using TimeTrackingService.Domain.Employees.ValueObjects;

namespace TimeTrackingService.Domain.Employees;

/// <summary>
/// Record thath store rate and date on which the rate takes effect.
/// </summary>
public sealed record HourlyRatePeriod(HourlyRate Rate, DateOnly EffectiveFrom);
