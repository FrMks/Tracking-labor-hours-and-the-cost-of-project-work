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
        return Result.Success<Employee, Error>(new Employee(id, fullName, department));
    }

}
