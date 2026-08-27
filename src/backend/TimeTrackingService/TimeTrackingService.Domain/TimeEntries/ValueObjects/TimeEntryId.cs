namespace TimeTrackingService.Domain.TimeEntries.ValueObjects;

public sealed record TimeEntryId
{
    private TimeEntryId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static TimeEntryId NewTimeEntryId() => new(Guid.NewGuid());

    public static TimeEntryId Empty() => new(Guid.Empty);

    public static TimeEntryId FromValue(Guid value) => new(value);

    public static implicit operator Guid(TimeEntryId timeEntryId) => timeEntryId.Value;
}
