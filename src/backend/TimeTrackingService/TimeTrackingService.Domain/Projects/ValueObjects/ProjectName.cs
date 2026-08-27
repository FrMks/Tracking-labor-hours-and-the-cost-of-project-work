using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain;

namespace TimeTrackingService.Domain.Projects.ValueObjects;

public sealed record ProjectName
{
    private ProjectName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ProjectName, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(
                "project.name.required",
                "Project name cannot be empty.",
                nameof(value));
        }

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > LengthConstants.LENGTH200)
        {
            return Error.Validation(
                "project.name.length.invalid",
                $"Project name cannot be longer than {LengthConstants.LENGTH200} characters.",
                nameof(value));
        }

        return Result.Success<ProjectName, Error>(new ProjectName(normalizedValue));
    }
}
