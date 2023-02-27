namespace GhostConsole.Service;

public interface IPhotoService
{
    public Task<IEnumerable<IPhoto>> GetAllAsync();
    public Task<IEnumerable<IPhoto>> GetFromAlbumAsync(string albumId);
}