namespace GhostConsole;

public static class ConsoleHelper
{
    public static IAppSettings AppConfig { get; } = InitAppConfig();

    public static string AlbumsBaseUri => $"{AppConfig.Api.BaseUri}/{AppConfig.Api.AlbumsUri}";
    public static string PhotosBaseUri => $"{AppConfig.Api.BaseUri}/{AppConfig.Api.PhotosUri}";
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

    public static async Task<int> RunAlbumAndPhotoPrompts(IServiceProvider services)
    {
        // Get album data & run status animation
        var albumService = services.GetRequiredService<IAlbumService>();
        var albums = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("[grey66]Fetching albums..[/]", async _ =>
            {
                await Task.Delay(3500);
                return await albumService.GetAllAsync();
            });

        if (albums.IsNullOrEmpty())
        {
            WriteError("No albums returned from service.");
            return Environment.ExitCode;
        }

        var selectedAlbum = AskAlbumSelection(albums);
        if (!AskConfirmation($$"""Get photos for album "[yellow]{{selectedAlbum}}[/]"?"""))
            return Environment.ExitCode;

        // Get photos data (by selected albumId) & run status animation
        var photoService = services.GetRequiredService<IPhotoService>();
        var photos = await AnsiConsole.Status()
            .Spinner(Spinner.Known.Aesthetic)
            .StartAsync("[grey66]Fetching photos..[/]", async _ =>
            {
                await Task.Delay(3500);
                var albumId = selectedAlbum.Split("-")[0].ExtractDigits();
                return await photoService.GetFromAlbumAsync(albumId);
            });

        // Write the selected album's photos JSON and wrap up console process.
        return WritePhotosJson(photos, selectedAlbum);
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

    private static int WritePhotosJson(IEnumerable<IPhoto> photos, string selectedAlbum)
    {
        if (photos.IsNullOrEmpty())
        {
            WriteError("No photos returned from service.");
            return Environment.ExitCode;
        }

        var displayPhotos = photos.Select(p => new { p.Id, p.Title }).ToList();
        var jsonContent = displayPhotos.ToJsonText();

        AnsiConsole.WriteLine();
        AnsiConsole.Write(
            new Panel(jsonContent)
                .Header($$"""*** {{displayPhotos.Count}} photos for album "{{selectedAlbum}}" ***""")
                .Collapse()
                .Padding(new Padding(2, 1, 2, 1))
                .RoundedBorder()
                .BorderColor(Color.Yellow));

        return Environment.ExitCode;
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

    private static string AskAlbumSelection(IEnumerable<IAlbum> albums)
    {
        var albumChoices = albums.Select(a => a.DisplayTitle()).ToArray();

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]{albumChoices.Length}[/] albums found. Select to view photos.")
                .PageSize(10)
                .MoreChoicesText("[grey54](Move up and down to reveal more albums)[/]")
                .AddChoices(albumChoices));
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
}