using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain;

namespace TimeTrackingService.Domain.Employees.ValueObjects;

public sealed record DepartmentName
{
    private DepartmentName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<DepartmentName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(
                "employee.department.required",
                "Department cannot be empty.");
        }

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > LengthConstants.LENGTH150)
        {
            return Error.Validation(
                "employee.department.length.invalid",
                $"Department cannot be longer than {LengthConstants.LENGTH150} characters.");
        }

        return Result.Success<DepartmentName, Error>(new DepartmentName(normalizedValue));
    }
}
