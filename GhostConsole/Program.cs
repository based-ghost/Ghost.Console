// Set the console output encoding to UTF8 (needed to render Spectre.Console spinners)
Console.OutputEncoding = Encoding.UTF8;

var builder = new HostBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddHttpClient<IAlbumService, AlbumService>(c => c.BaseAddress = new Uri(ConsoleHelper.AlbumsBaseUri));
        services.AddHttpClient<IPhotoService, PhotoService>(c => c.BaseAddress = new Uri(ConsoleHelper.PhotosBaseUri));
    }).UseConsoleLifetime();

var host = builder.Build();

using (var serviceScope = host.Services.CreateScope())
{
    try
    {
        // Init setup & info messages
        ConsoleHelper.WriteMainTitle();

        // Check console supports interaction & user confirmed continue
        if (!ConsoleHelper.EnvironmentIsVerified)
            return Environment.ExitCode;

        // Run console prompts that trigger API requests & display results
        var services = serviceScope.ServiceProvider;
        return await ConsoleHelper.RunAlbumAndPhotoPrompts(services);
    }
    catch (Exception ex)
    {
        ConsoleHelper.WriteError(ex.Message);
    }
}

return Environment.ExitCode;