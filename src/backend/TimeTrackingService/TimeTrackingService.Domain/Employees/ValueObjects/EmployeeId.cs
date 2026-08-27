namespace TimeTrackingService.Domain.Employees.ValueObjects;

public sealed record EmployeeId
{
    private EmployeeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EmployeeId NewEmployeeId() => new(Guid.NewGuid());

    public static EmployeeId Empty() => new(Guid.Empty);

    public static EmployeeId FromValue(Guid value) => new(value);

    public static implicit operator Guid(EmployeeId employeeId) => employeeId.Value;
}
