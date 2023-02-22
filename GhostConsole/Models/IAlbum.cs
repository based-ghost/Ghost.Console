namespace GhostConsole.Models;

public interface IAlbum
{
    public int    Id     { get; set; }
    public int    UserId { get; set; }
    public string Title  { get; set; }
}