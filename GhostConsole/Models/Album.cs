﻿namespace GhostConsole.Models;

public class Album : IAlbum
{
    public int    Id    { get; set; }
    public string Title { get; set; } = null!;

    public string DisplayTitle => $"{Id} - {Title.Truncate(25)?.TrimEnd()}";
}