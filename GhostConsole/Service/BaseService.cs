namespace GhostConsole.Service;

public abstract class BaseService
{
    private protected readonly HttpClient _httpClient;
    private protected readonly JsonSerializerOptions _jsonOptions;

    protected BaseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };
    }

    protected async Task<TResult?> GetAsync<TResult>(string? uri = null) => await _httpClient.GetFromJsonAsync<TResult>(uri, _jsonOptions);
}
