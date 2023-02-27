# `Ghost.Console`
This is a [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) console application built with [Spectre.Console](https://spectreconsole.net/) that demonstrates simple API communication via endpoints from [JSONPlaceholder](https://jsonplaceholder.typicode.com/).


![demo](./demo/GhostConsole.gif)


## Tech stack

- [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) - .NET 7 SDK.
- [Spectre.Console](https://spectreconsole.net/) - a robust library to help create beautiful console applications.
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/) - manages/binds configuration from `appsettings.json` to concrete types.
- [xUnit.net](https://xunit.net/) - handles unit testing for the console application project.

## Building & running locally

### Prerequisites

1. Install the [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0).
2. Clone this repository to your local machine.

### Building & running the project

> `dotnet` commands can be issued via `Windows Terminal`, `Powershell`, `Command Prompt`, etc. - the GIF above is using `Windows Terminal`.

1. <strong>Build the projects:</strong> run `dotnet build` in the `root` folder.
   > Directory containing the `GhostConsole.sln` file. Alternatively, you can open the solution in VS/VSCode and execute build commands there.
2. <strong>Run the console app:</strong> run `dotnet run` in the `./GhostConsole` folder.
   > Directory containing the `GhostConsole.csproj` file. Alternatively, you can open the application in VS/VSCode and run the project from there.
3. <strong>Run the unit tests:</strong> run `dotnet test` in the `./GhostConsole.Tests` folder.
   > Directory containing the `GhostConsole.Tests.csproj` file. Alternatively, you can open the solution in VS/VSCode and right-click => `Run Tests` on the `GhostConsole.Tests` project.