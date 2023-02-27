namespace GhostConsole;

public static class ConsoleHelper
{
    public static IAppSettings  AppConfig    { get; } = InitAppConfig();
    public static IAlbumService AlbumService { get; } = InitAlbumService();
    public static IPhotoService PhotoService { get; } = InitPhotoService();

    public static bool EnvironmentIsVerified => IsConsoleInteractive() && AskConfirmation("Get album list?");

    public static void WriteMainTitle()
    {
        var title = new FigletText(AppConfig.Title)
            .Color(Color.Yellow)
            .Centered();

        var appInfo = new Rule($"Displays photo album data from [green][link={AppConfig.Api.BaseUri}]{AppConfig.Api.Name}[/][/]")
            .Centered();

        AnsiConsole.WriteLine();
        AnsiConsole.Write(title);
        AnsiConsole.WriteLine();
        AnsiConsole.Write(appInfo);
        AnsiConsole.WriteLine();
    }

    public static async Task ExecuteAlbumAndPhotoPrompts()
    {
        var albums = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("[grey66]Fetching albums..[/]", async _ =>
            {
                // Delays added to let spinner animation play
                await Task.Delay(3500);
                return await AlbumService.GetAllAsync();
            });

        if (albums.IsNullOrEmpty())
        {
            WriteError("No albums returned from service.");
            return;
        }

        // SelectionPrompt.AddChoices() accepts only a string array
        var albumChoices = albums.Select(a => a.DisplayTitle).ToArray();

        var selectedAlbum = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]{albumChoices.Length}[/] albums found. Select to view photos.")
                .PageSize(10)
                .MoreChoicesText("[grey54](Move up and down to reveal more albums)[/]")
                .AddChoices(albumChoices));

        if (!AskConfirmation($$"""Get photos for album "[yellow]{{selectedAlbum}}[/]"?"""))
            return;

        var photos = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("[grey66]Fetching photos..[/]", async _ =>
            {
                // Delays added to let spinner animation play
                await Task.Delay(3500);
                var albumId = selectedAlbum.Split("-")[0].ExtractDigits();
                return await PhotoService.GetFromAlbumAsync(albumId);
            });

        if (photos.IsNullOrEmpty())
        {
            WriteError("No photos returned from service.");
            return;
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(
            new Panel(photos.ToJsonText())
                .Header($$"""*** {{photos?.Count()}} photos for album "{{selectedAlbum}}" ***""")
                .Collapse()
                .Padding(new Padding(2, 1, 2, 1))
                .RoundedBorder()
                .BorderColor(Color.Yellow));
    }

    public static void Write(string message, Color? color = null)
    {
        var value = (color is Color) ? $"[{color}]{message}[/]" : message;
        AnsiConsole.Markup(value);
    }

    public static void WriteError(string message)
    {
        Write(message, Color.Red);
    }

    private static bool IsConsoleInteractive()
    {
        var isInteractive = AnsiConsole.Profile.Capabilities.Interactive;
        if (!isInteractive)
            WriteError("Environment does not support interaction.");

        return isInteractive;
    }

    private static bool AskConfirmation(string message)
    {
        var isConfirmed = AnsiConsole.Confirm(message);
        if (!isConfirmed)
            WriteError("Process terminated by user.");

        return isConfirmed;
    }

    private static HttpClient BuildHttpClient(string uri)
    {
        var baseAddress = new Uri($"{AppConfig.Api.BaseUri}/{uri}");
        return new HttpClient { BaseAddress = baseAddress };
    }

    private static IAppSettings InitAppConfig()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<AppSettings>();

        return config ?? throw new NullReferenceException($"Could not bind appsettings.json to {nameof(AppSettings)}");
    }

    private static IAlbumService InitAlbumService() => new AlbumService(BuildHttpClient(AppConfig.Api.AlbumsUri));
    private static IPhotoService InitPhotoService() => new PhotoService(BuildHttpClient(AppConfig.Api.PhotosUri));
}