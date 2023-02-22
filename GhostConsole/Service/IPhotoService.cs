namespace GhostConsole.Service;

public interface IPhotoService
{
    public Task<IEnumerable<IAlbum>> GetAlbumsAsync();
    public Task<IEnumerable<IPhoto>> GetAlbumPhotosAsync(string albumId);
}