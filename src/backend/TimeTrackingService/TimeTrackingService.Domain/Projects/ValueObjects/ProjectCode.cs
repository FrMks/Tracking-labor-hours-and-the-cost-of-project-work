using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shared;
using TimeTrackingService.Domain;

namespace TimeTrackingService.Domain.Projects.ValueObjects;

public sealed record ProjectCode
{
    private static readonly Regex ValidFormat = new(
        "^[\\p{L}]+-\\d{3}$",
        RegexOptions.CultureInvariant);

    private ProjectCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<ProjectCode, Error> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(
                "project.code.required",
                "Project code cannot be empty.",
                nameof(value));
        }

        var normalizedValue = value.Trim();

        // The task gives an example, but does not prescribe a code format explicitly.
        if (!ValidFormat.IsMatch(normalizedValue))
        {
            return Error.Validation(
                "project.code.format.invalid",
                "Project code must contain letters, a hyphen and three digits.",
                nameof(value));
        }

        return Result.Success<ProjectCode, Error>(new ProjectCode(normalizedValue));
    }
}
