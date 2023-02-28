// Set output encoding to UTF8 - needed to render Spectre.Console spinners
Console.OutputEncoding = Encoding.UTF8;

// Adds HttpClientFactory/registers transient services - services request HttpClient from DI
var host = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddHttpClient<IAlbumService, AlbumService>(c => c.BaseAddress = new Uri(ConsoleHelper.AlbumsBaseUri));
        services.AddHttpClient<IPhotoService, PhotoService>(c => c.BaseAddress = new Uri(ConsoleHelper.PhotosBaseUri));
    })
    .Build();

// Init setup & info messages
ConsoleHelper.WriteMainTitle();

// Check console supports interaction & user confirmed continue
if (!ConsoleHelper.EnvironmentIsVerified)
    return Environment.ExitCode;

// Run console prompts that trigger API requests & display results
return await ConsoleHelper.RunAlbumAndPhotoPrompts(host.Services);