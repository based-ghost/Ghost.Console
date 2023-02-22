namespace GhostConsole.Data;

public class ApiClient : IApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(string baseUri)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUri)
        };

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<TResult?> GetAsync<TResult>(string relativeUri)
    {
        var res = await _httpClient.GetAsync(relativeUri);
        res.EnsureSuccessStatusCode();

        return await res.Content.ReadFromJsonAsync<TResult>(_jsonOptions);
    }
}