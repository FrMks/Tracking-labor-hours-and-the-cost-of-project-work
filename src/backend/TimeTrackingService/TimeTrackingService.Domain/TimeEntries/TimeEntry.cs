using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain.Employees.ValueObjects;
using TimeTrackingService.Domain.Projects.ValueObjects;
using TimeTrackingService.Domain.TimeEntries.ValueObjects;

namespace TimeTrackingService.Domain.TimeEntries;

public sealed class TimeEntry
{
    private TimeEntry(
        TimeEntryId id,
        EmployeeId employeeId,
        ProjectId projectId,
        DateOnly entryDate,
        Hours hours,
        string? comment,
        long version,
        DateTime createdAtUtc,
        DateTime? updatedAtUtc)
    {
        Id = id;
        EmployeeId = employeeId;
        ProjectId = projectId;
        EntryDate = entryDate;
        Hours = hours;
        Comment = comment;
        Version = version;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    public TimeEntryId Id { get; private set; }

    public EmployeeId EmployeeId { get; private set; }

    public ProjectId ProjectId { get; private set; }

    public DateOnly EntryDate { get; private set; }

    public Hours Hours { get; private set; }

    public string? Comment { get; private set; }

    public long Version { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public DateTime? UpdatedAtUtc { get; private set; }

    // The specification does not clarify whether duplicate entries for one employee/project/day are allowed.
    // Until a separate rule appears, the domain permits multiple entries and validates the daily total elsewhere.
    public static Result<TimeEntry, Error> Create(
        TimeEntryId id,
        EmployeeId employeeId,
        ProjectId projectId,
        DateOnly entryDate,
        Hours hours,
        string? comment,
        DateTime createdAtUtc)
    {
        var normalizedComment = string.IsNullOrWhiteSpace(comment)
            ? null
            : comment.Trim();

        if (normalizedComment?.Length > LengthConstants.LENGTH1000)
        {
            return Error.Validation(
                "time-entry.comment.length.invalid",
                $"Comment cannot be longer than {LengthConstants.LENGTH1000} characters.",
                nameof(comment));
        }

        return Result.Success<TimeEntry, Error>(new TimeEntry(
            id,
            employeeId,
            projectId,
            entryDate,
            hours,
            normalizedComment,
            version: 0,
            createdAtUtc,
            updatedAtUtc: null));
    }
}
