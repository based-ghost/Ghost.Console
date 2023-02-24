namespace GhostConsole.Models;

public interface IAlbum
{
    public int    Id           { get; set; }
    public string Title        { get; set; }
    public string DisplayTitle { get; }
}