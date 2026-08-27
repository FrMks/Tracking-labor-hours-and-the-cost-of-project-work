using CSharpFunctionalExtensions;
using Shared;

namespace TimeTrackingService.Domain.Employees.ValueObjects;

public sealed record FullName
{
    private const int MaxPartLength = 100;

    private FullName(string lastName, string firstName, string? patronymic)
    {
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
    }

    public string LastName { get; }

    public string FirstName { get; }

    public string? Patronymic { get; }

    public string DisplayValue => string.Join(
        " ",
        new[] { LastName, FirstName, Patronymic }.Where(part => !string.IsNullOrWhiteSpace(part)));

    public static Result<FullName, Error> Create(
        string lastName,
        string firstName,
        string? patronymic)
    {
        var normalizedLastNameResult = NormalizePart(lastName, "last-name");
        if (normalizedLastNameResult.IsFailure)
        {
            return normalizedLastNameResult.Error;
        }

        var normalizedFirstNameResult = NormalizePart(firstName, "first-name");
        if (normalizedFirstNameResult.IsFailure)
        {
            return normalizedFirstNameResult.Error;
        }

        var normalizedPatronymicResult = NormalizeOptionalPart(patronymic, "patronymic");
        if (normalizedPatronymicResult.IsFailure)
        {
            return normalizedPatronymicResult.Error;
        }

        return Result.Success<FullName, Error>(new FullName(
            normalizedLastNameResult.Value,
            normalizedFirstNameResult.Value,
            normalizedPatronymicResult.Value));
    }

    private static Result<string, Error> NormalizePart(string? value, string partName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Error.Validation(
                $"employee.full-name.{partName}.required",
                $"{partName} cannot be empty.");
        }

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > MaxPartLength)
        {
            return Error.Validation(
                $"employee.full-name.{partName}.length.invalid",
                $"{partName} cannot be longer than {MaxPartLength} characters.");
        }

        return Result.Success<string, Error>(normalizedValue);
    }

    private static Result<string?, Error> NormalizeOptionalPart(string? value, string partName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Success<string?, Error>(null);
        }

        var normalizedValue = value.Trim();
        if (normalizedValue.Length > MaxPartLength)
        {
            return Error.Validation(
                $"employee.full-name.{partName}.length.invalid",
                $"{partName} cannot be longer than {MaxPartLength} characters.");
        }

        return Result.Success<string?, Error>(normalizedValue);
    }
}
