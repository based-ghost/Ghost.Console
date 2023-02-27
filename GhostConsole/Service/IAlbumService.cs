namespace GhostConsole.Service;

public interface IAlbumService
{
    public Task<IEnumerable<IAlbum>> GetAllAsync();
}