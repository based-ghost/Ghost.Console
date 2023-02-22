namespace GhostConsole.Tests.Service;

public class PhotoServiceTests
{
    [Fact]
    public async Task CanGetAlbumsAsync()
    {
        var albums = await ConsoleHelper.PhotoService.GetAlbumsAsync();
        AssertNotNullOrEmpty(albums);
    }

    [Fact]
    public async Task CanGetAlbumPhotosAsync()
    {
        // Get last album in results to ensure Id > 0
        var albums = await ConsoleHelper.PhotoService.GetAlbumsAsync();
        var albumId = albums.LastOrDefault()?.Id;
        var castAlbumId = albumId.GetValueOrDefault();

        Assert.IsType<int>(albumId);
        Assert.InRange(castAlbumId, 1, int.MaxValue);

        var photos = await ConsoleHelper.PhotoService.GetAlbumPhotosAsync(castAlbumId.ToString());
        AssertNotNullOrEmpty(photos);
    }

    private static void AssertNotNullOrEmpty<T>(IEnumerable<T> results)
    {
        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}