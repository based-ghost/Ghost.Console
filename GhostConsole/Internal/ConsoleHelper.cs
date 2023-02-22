namespace GhostConsole;

public static class ConsoleHelper
{
    public static IAppSettings  AppConfig    { get; } = InitAppConfig();
    public static IPhotoService PhotoService { get; } = InitPhotoService();
    public static bool EnvironmentIsVerified => IsConsoleInteractive() && AskConfirmation("Get album list?");

    public static void WriteMainTitle()
    {
        var title = new FigletText(AppConfig.Title)
            .Color(Color.Yellow)
            .Centered();

        var appInfo = new Rule($"Displays photo album data from [blue][link={AppConfig.Api.BaseUri}]{AppConfig.Api.Name}[/][/]")
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
            .StartAsync("[yellow]Fetching albums..[/]", async _ =>
            {
                // Delays added to let animation play - also, depending on the workflow,
                // requests can sometimes complete too quickly to the point user experience
                // suffers (I think humans percieve anything <= 200ms as instantaneous)
                await Task.Delay(4000);
                return await PhotoService.GetAlbumsAsync();
            });

        if (!albums?.Any() ?? false)
        {
            WriteError("No albums returned from service.");
            return;
        }

        var albumChoices = albums?.Select(a => $"{a.Id} - {a.Title}").ToArray() ?? Array.Empty<string>();
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"Album count: [yellow]{albumChoices.Length}[/]");

        var selectedAlbum = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an album to view photos.")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more albums)[/]")
                .AddChoices(albumChoices));

        AnsiConsole.MarkupLine("Selected album: [yellow]{0}[/]", selectedAlbum);

        if (!AskConfirmation("Get photos for this album?"))
            return;

        var photos = await AnsiConsole.Status()
            .StartAsync("[yellow]Fetching photos..[/]", async _ =>
            {
                await Task.Delay(4000);
                var albumId = selectedAlbum.Split("-")[0].Trim();
                return await PhotoService.GetAlbumPhotosAsync(albumId);
            });

        if (!photos?.Any() ?? false)
        {
            WriteError("No photos returned from service.");
            return;
        }

        AnsiConsole.WriteLine();
        var photosJson = JsonSerializer.Serialize(photos);
        var jsonText = new JsonText(photosJson);

        AnsiConsole.Write(
            new Panel(jsonText)
                .Header($"JSON for {photos?.Count()} photos")
                .Collapse()
                .Padding(new Padding(1, 1, 1, 1))
                .RoundedBorder()
                .BorderColor(Color.Yellow));
    }

    public static void WriteLog(string message) => Write(message);
    public static void WriteError(string message) => Write(message, Color.Red);
    public static void WriteSuccess(string message) => Write(message, Color.Green);

    private static void Write(string message, Color? color = null)
    {
        var value = (color is Color) ? $"[{color}]{message}[/]" : message;
        AnsiConsole.MarkupLine(value);
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
            WriteLog("Process terminated by user.");

        return isConfirmed;
    }

    private static IPhotoService InitPhotoService()
    {
        var apiClient = new ApiClient(AppConfig.Api.BaseUri);
        return new PhotoService(apiClient);
    }

    private static IAppSettings InitAppConfig()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<AppSettings>();

        return config ?? throw new NullReferenceException($"Could not bind settings to {nameof(AppSettings)} object");
    }
}