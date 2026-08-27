using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain.Employees.ValueObjects;

namespace TimeTrackingService.Domain.Employees;

public sealed class Employee
{
    private readonly List<HourlyRatePeriod> _hourlyRates = [];

    private Employee(
        EmployeeId id,
        FullName fullName,
        DepartmentName department)
    {
        Id = id;
        FullName = fullName;
        Department = department;
    }

    public EmployeeId Id { get; private set; }

    public FullName FullName { get; private set; }

    public DepartmentName Department { get; private set; }

    public IReadOnlyCollection<HourlyRatePeriod> HourlyRates => _hourlyRates.AsReadOnly();

    public static Result<Employee, Error> Create(
        EmployeeId id,
        FullName fullName,
        DepartmentName department)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(fullName);
        ArgumentNullException.ThrowIfNull(department);

        return Result.Success<Employee, Error>(new Employee(id, fullName, department));
    }

    public UnitResult<Error> Rename(FullName fullName)
    {
        ArgumentNullException.ThrowIfNull(fullName);
        FullName = fullName;
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> ChangeDepartment(DepartmentName department)
    {
        ArgumentNullException.ThrowIfNull(department);
        Department = department;
        return UnitResult.Success<Error>();
    }

    public UnitResult<Error> AddOrReplaceHourlyRate(
        HourlyRate rate,
        DateOnly effectiveFrom)
    {
        ArgumentNullException.ThrowIfNull(rate);

        var existingIndex = _hourlyRates.FindIndex(period => period.EffectiveFrom == effectiveFrom);
        var period = new HourlyRatePeriod(rate, effectiveFrom);

        if (existingIndex >= 0)
        {
            _hourlyRates[existingIndex] = period;
        }
        else
        {
            _hourlyRates.Add(period);
        }

        _hourlyRates.Sort(static (left, right) => left.EffectiveFrom.CompareTo(right.EffectiveFrom));
        return UnitResult.Success<Error>();
    }

    public Result<HourlyRate, Error> GetHourlyRateOn(DateOnly date)
    {
        var period = _hourlyRates
            .Where(period => period.EffectiveFrom <= date)
            .MaxBy(period => period.EffectiveFrom);

        return period is null
            ? Error.Validation(
                "employee.hourly-rate.not-defined",
                $"No hourly rate is defined for {date:yyyy-MM-dd}.")
            : Result.Success<HourlyRate, Error>(period.Rate);
    }
}
