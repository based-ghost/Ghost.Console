namespace GhostConsole.Tests.Service;

public class ServiceTests
{
    #region Fields

    /// <summary>
    /// HttpClient method to mock.
    /// Signature: <see cref="SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)" />.
    /// </summary>
    private const string _sendAsyncMethod = "SendAsync";

    #endregion Fields

    #region Album Service Tests

    [Fact]
    public async Task Should_Return_Albums()
    {
        // Arrange
        var mockAlbums = Enumerable.Range(1, 3).Select(i => new Album { Id = i, UserId = 1, Title = $"Album title {i}" });
        var mockHandler = GetMockHttpMsgHandler(mockAlbums);
        var baseAddress = new Uri(ConsoleHelper.AlbumsBaseUri);
        var mockHttpClient = new HttpClient(mockHandler.Object) { BaseAddress = baseAddress };
        var albumService = new AlbumService(mockHttpClient);

        /// Act
        var albums = await albumService.GetAllAlbumsAsync();

        /// Assert
        Assert.NotNull(albums);
        Assert.NotEmpty(albums);
        VerifyMockHttpRequest(mockHandler, baseAddress);
    }

    #endregion AlbumService Tests

    #region Photo Service Tests

    [Fact]
    public async Task Should_Return_Photos()
    {
        // Arrange
        var mockPhotos = Enumerable.Range(1, 3).Select(i => new Photo { Id = i, AlbumId = 1, Title = $"Photo title {i}" });
        var mockHandler = GetMockHttpMsgHandler(mockPhotos);
        var baseAddress = new Uri(ConsoleHelper.PhotosBaseUri);
        var mockHttpClient = new HttpClient(mockHandler.Object) { BaseAddress = baseAddress };
        var photoService = new PhotoService(mockHttpClient);

        /// Act
        var photos = await photoService.GetAllPhotosAsync();

        /// Assert
        Assert.NotNull(photos);
        Assert.NotEmpty(photos);
        VerifyMockHttpRequest(mockHandler, baseAddress);
    }

    #endregion Photo Service Tests

    #region Private Helpers

    private static Mock<HttpMessageHandler> GetMockHttpMsgHandler<T>(T response)
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
                _sendAsyncMethod,
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
                _sendAsyncMethod,
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());
    }

    #endregion Private Helpers
}
