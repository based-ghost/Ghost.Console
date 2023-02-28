namespace GhostConsole.Tests.Service;

public class ServiceTests
{
    #region AlbumService Tests

    [Fact]
    public async Task Should_ReturnAlbums()
    {
        // Arrange
        var mockAlbums = Enumerable.Range(1, 3).Select(i => new Album { Id = i, UserId = 1, Title = $"Album title {i}" });
        var mockHandler = GetMockHttpHandler(mockAlbums);
        var baseAddress = new Uri(ConsoleHelper.AlbumsBaseUri);
        var mockHttpClient = new HttpClient(mockHandler.Object) { BaseAddress = baseAddress };
        var albumService = new AlbumService(mockHttpClient);

        /// Act
        var albums = await albumService.GetAllAsync();

        /// Assert
        Assert.NotNull(albums);
        Assert.NotEmpty(albums);
        VerifyMockHttpRequest(mockHandler, baseAddress);
    }

    #endregion AlbumService Tests

    #region PhotoService Tests

    [Fact]
    public async Task Should_ReturnPhotos()
    {
        // Arrange
        var mockPhotos = Enumerable.Range(1, 3).Select(i => new Photo { Id = i, AlbumId = 1, Title = $"Photo title {i}" });
        var mockHandler = GetMockHttpHandler(mockPhotos);
        var baseAddress = new Uri(ConsoleHelper.PhotosBaseUri);
        var mockHttpClient = new HttpClient(mockHandler.Object) { BaseAddress = baseAddress };
        var photoService = new PhotoService(mockHttpClient);

        /// Act
        var photos = await photoService.GetAllAsync();

        /// Assert
        Assert.NotNull(photos);
        Assert.NotEmpty(photos);
        VerifyMockHttpRequest(mockHandler, baseAddress);
    }

    #endregion PhotoService Tests

    #region Private Methods

    private static Mock<HttpMessageHandler> GetMockHttpHandler<T>(T response)
    {
        var mockHandler = new Mock<HttpMessageHandler>();
        var mockResponse = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(response))
        };

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return mockHandler;
    }

    private static void VerifyMockHttpRequest(Mock<HttpMessageHandler> mockHandler, Uri uri)
    {
        mockHandler
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());
    }

    #endregion Private Methods
}
