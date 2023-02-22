namespace GhostConsole.Extensions;

public static class CollectionExtensions
{
    public static IEnumerable<T> DefaultToEmptyIfNull<T>(this IEnumerable<T>? value)
    {
        return value ?? Enumerable.Empty<T>();
    }
}
