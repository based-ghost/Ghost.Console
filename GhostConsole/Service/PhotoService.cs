namespace GhostConsole.Service;

public class PhotoService : IPhotoService
{
    private readonly IApiClient _apiClient;

    public PhotoService(IApiClient apiClient) => _apiClient = apiClient;

    public async Task<IEnumerable<IAlbum>> GetAlbumsAsync()
    {
        const string relativeUri = "albums";
        var result = await _apiClient.GetAsync<IEnumerable<Album>>(relativeUri);
        return result.EmptyIfNull();
    }

    public async Task<IEnumerable<IPhoto>> GetAlbumPhotosAsync(string albumId)
    {
        var relativeUri = $"photos?{nameof(albumId)}={albumId}";
        var result = await _apiClient.GetAsync<IEnumerable<Photo>>(relativeUri);
        return result.EmptyIfNull();
    }
}
