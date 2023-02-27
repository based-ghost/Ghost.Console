namespace GhostConsole.Service;

public class AlbumService : BaseService, IAlbumService
{
    public AlbumService(HttpClient httpClient) : base(httpClient) { }

    public async Task<IEnumerable<IAlbum>> GetAllAsync()
    {
        var result = await GetAsync<IEnumerable<Album>>();
        return result.EmptyIfDefault();
    }
}
