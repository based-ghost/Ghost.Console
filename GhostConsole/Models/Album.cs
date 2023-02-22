namespace GhostConsole.Models;

public class Album : IAlbum
{
    public int    Id     { get; set; }
    public int    UserId { get; set; }
    public string Title  { get; set; } = null!;
}