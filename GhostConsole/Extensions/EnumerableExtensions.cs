namespace GhostConsole.Extensions;

/// <summary>
/// IEnumerable extensions.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Returns true if collection is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source?.Any() != true;

    /// <summary>
    /// Returns an empty IEnumerable<out T> if source is null.
    /// </summary>
    public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? source) => source ?? Enumerable.Empty<T>();

    /// <summary>
    /// Creates a JsonText class from an IEnumerable<out T>
    /// JsonText is a renderable piece of JSON text from package Spectre.Console.Json.
    /// </summary>
    public static JsonText ToJsonText<T>(this IEnumerable<T>? source)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        return new JsonText(JsonSerializer.Serialize(source));
    }
}
