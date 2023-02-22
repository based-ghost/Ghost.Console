namespace GhostConsole.Models;

public sealed class AppSettings : IAppSettings
{
    public required string         Title { get; set; }
    public required NestedSettings Api   { get; set; } = null!;
}

public sealed class NestedSettings : INestedSettings
{
    public required string Name    { get; set; } = null!;
    public required string BaseUri { get; set; } = null!;
}