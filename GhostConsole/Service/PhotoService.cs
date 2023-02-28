namespace GhostConsole.Service;

public class PhotoService : BaseService, IPhotoService
{
    public PhotoService(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<IPhoto>> GetAllPhotosAsync()
    {
        var result = await GetAsync<IEnumerable<Photo>>();
        return result.EmptyIfDefault();
    }

    public async Task<IEnumerable<IPhoto>> GetPhotosByAlbumIdAsync(string albumId)
    {
        var uri = $"?{nameof(albumId)}={albumId}";
        var result = await GetAsync<IEnumerable<Photo>>(uri);
        return result.EmptyIfDefault();
    }
}
