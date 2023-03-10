# `Ghost.Console`
This is a [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) console application built with [Spectre.Console](https://spectreconsole.net/) that demonstrates simple API communication via endpoints from [JSONPlaceholder](https://jsonplaceholder.typicode.com/).


![demo](./demo/GhostConsole.gif)


## Tech stack

- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0) - .NET 7.0 SDK.
- [Spectre.Console](https://spectreconsole.net/) - robust library that helps create beautiful console applications.
- [xUnit.net](https://xunit.net/) - unit testing for the console application project.
- [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/) - manages hosting and startup infrastructure for the application.
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/) - binds configuration from `appsettings.json` to concrete types.
- [Microsoft.Extensions.Http](https://www.nuget.org/packages/Microsoft.Extensions.Http/) - enables the HttpClient factory pattern for configuring/retrieving named HttpClients in a composable way.

## Building & running locally

### Prerequisites

1. Install the [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0).
2. Clone this repository to your local machine.

### Building & running the project

> `dotnet` commands can be issued via Windows Terminal, Powershell, Command Prompt, etc. (the GIF above is using Windows Terminal). Alternatively, you can open the solution in VS or VSCode and execute commands there.

1. <strong>Build the projects:</strong> run `dotnet build` in the `root` folder.
   > Directory containing the `GhostConsole.sln` file.
2. <strong>Run the console app:</strong> run `dotnet run` in the `./GhostConsole` folder.
   > Directory containing the `GhostConsole.csproj` file.
3. <strong>Run the unit tests:</strong> run `dotnet test` in the `./GhostConsole.Tests` folder.
   > Directory containing the `GhostConsole.Tests.csproj` file.