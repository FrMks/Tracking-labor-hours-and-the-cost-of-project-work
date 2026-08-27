namespace TimeTrackingService.Domain.Projects.ValueObjects;

public sealed record ProjectId
{
    private ProjectId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProjectId NewProjectId() => new(Guid.NewGuid());

    public static ProjectId Empty() => new(Guid.Empty);

    public static ProjectId FromValue(Guid value) => new(value);

    public static implicit operator Guid(ProjectId projectId) => projectId.Value;
}
