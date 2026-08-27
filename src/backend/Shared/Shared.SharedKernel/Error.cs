namespace Shared;

public class Error
{
    private const string Separator = "||";

    private Error(string code, string message, ErrorType type, string? invalidField = null)
    {
        Code = code;
        Message = message;
        Type = type;
        InvalidField = invalidField;
    }

    public static Error NotFound(string? code, string message, Guid? id = null) =>
        new(code ?? "record.not.found", message, ErrorType.NotFound);

    public static Error Validation(string? code, string message, string? invalidField = null) =>
        new(code ?? "value.is.invalid", message, ErrorType.Validation, invalidField);

    public static Error Conflict(string? code, string message) =>
        new(code ?? "value.is.conflict", message, ErrorType.Conflict);

    public static Error Failure(string? code, string message) =>
        new(code ?? "failure", message, ErrorType.Failure);

    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(Separator);

        if (parts.Length < 3 || !Enum.TryParse(parts[2], out ErrorType type))
        {
            throw new ArgumentException("Invalid serialized format", nameof(serialized));
        }

        return new Error(parts[0], parts[1], type);
    }

    public string Code { get; set; }

    public string Message { get; set; }

    public ErrorType Type { get; set; }

    public string? InvalidField { get; set; }

    public string Serialize() => string.Join(Separator, Code, Message, Type);

    public Errors ToErrors() => this;
}
