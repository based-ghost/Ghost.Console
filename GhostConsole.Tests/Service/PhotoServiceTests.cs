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
        var albums = await ConsoleHelper.PhotoService.GetAlbumsAsync();
        var album = albums.FirstOrDefault();
        Assert.NotNull(album);

        var albumId = album!.Id.ToString();
        var photos = await ConsoleHelper.PhotoService.GetAlbumPhotosAsync(albumId);
        AssertNotNullOrEmpty(photos);
    }

    private static void AssertNotNullOrEmpty<T>(IEnumerable<T> results)
    {
        Assert.NotNull(results);
        Assert.NotEmpty(results);
    }
}