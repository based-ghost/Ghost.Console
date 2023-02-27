namespace GhostConsole.Extensions;

/// <summary>
/// IEnumerable extensions.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Returns true if enumerable is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source?.Any() != true;

    /// <summary>
    /// Returns empty if enumerable is null else same enumerable.
    /// </summary>
    public static IEnumerable<T> EmptyIfDefault<T>(this IEnumerable<T>? source) => source ?? Enumerable.Empty<T>();

    /// <summary>
    /// Creates a JsonText class from an enumerable.
    /// JsonText is a renderable piece of JSON text from package Spectre.Console.Json.
    /// </summary>
    public static JsonText ToJsonText<T>(this IEnumerable<T> source)
    {
        return source is null
            ? throw new ArgumentNullException(nameof(source))
            : new JsonText(JsonSerializer.Serialize(source));
    }
}
