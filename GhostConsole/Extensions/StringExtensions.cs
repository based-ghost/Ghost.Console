namespace GhostConsole.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Extracts only the numeric values from a string.
    /// </summary>
    public static string ExtractDigits(this string source) => string.Concat(source.Where(char.IsNumber));

    /// <summary>
    /// Truncates a string with specified suffix if length exceeds max length.
    /// </summary>
    public static string? Truncate(this string? source, int maxLength, string suffix = "..")
    {
        return source?.Length > maxLength
            ? source[..(maxLength - suffix.Length)] + suffix
            : source;
    }
}