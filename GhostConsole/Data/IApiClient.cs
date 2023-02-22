namespace GhostConsole.Data;

public interface IApiClient
{
    Task<TResult?> GetAsync<TResult>(string relativeUri);
}