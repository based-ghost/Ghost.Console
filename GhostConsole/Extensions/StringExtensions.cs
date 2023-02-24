namespace GhostConsole.Extensions;

/// <summary>
/// String extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Extracts only the numeric values from a string.
    /// </summary>
    public static string ExtractDigits(this string? source)
    {
        return !string.IsNullOrWhiteSpace(source)
            ? string.Concat(source.Where(char.IsNumber))
            : string.Empty;
    }

    /// <summary>
    /// Truncates a string with an ellipses if length exceeds max limit.
    /// </summary>
    public static string Truncate(this string? source, int maxChars)
    {
        if (string.IsNullOrEmpty(source))
            return string.Empty;

        return (source.Length > maxChars)
            ? $"{source[..maxChars].TrimEnd()}.."
            : source;
    }
}