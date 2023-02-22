namespace GhostConsole.Models;

public class Photo : IPhoto
{
    public int    Id    { get; set; }
    public string Title { get; set; } = null!;
}