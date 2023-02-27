namespace GhostConsole.Models;

public interface IAppSettings
{
    public string         Title { get; set; }
    public NestedSettings Api   { get; set; }
}

public interface INestedSettings
{
    public string Name      { get; set; }
    public string BaseUri   { get; set; }
    public string AlbumsUri { get; set; }
    public string PhotosUri { get; set; }
}