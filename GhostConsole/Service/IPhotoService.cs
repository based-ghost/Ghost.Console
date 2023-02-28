namespace GhostConsole.Service;

public interface IPhotoService
{
    public Task<IEnumerable<IPhoto>> GetAllPhotosAsync();
    public Task<IEnumerable<IPhoto>> GetPhotosByAlbumIdAsync(string albumId);
}