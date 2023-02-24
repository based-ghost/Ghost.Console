# `Ghost.Console`
This is a [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) console application built with [Spectre.Console](https://spectreconsole.net/) that demonstrates simple API communication via endpoints from [JSONPlaceholder](https://jsonplaceholder.typicode.com/).


![demo](./demo/GhostConsole.gif)


## Tech stack

- [.NET 7](https://dotnet.microsoft.com/download/dotnet/7.0) - .NET 7 SDK.
- [Spectre.Console](https://spectreconsole.net/) - a robust library to make beautiful console applications.
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/) - manages/binds configuration from `appsettings.json` to concrete types.
- [xUnit.net](https://xunit.net/) - handles unit testing for the console application project.

## Building & running locally

### Prerequisites

1. Install the [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0).
2. Clone this repository to your local machine.

### Building & running the project

1. Build the console application and test project by running `dotnet build` in the `GhostConsole` folder.
   > Located in the root folder (where the `GhostConsole.sln` file is found). Alternatively, you can open the solution in VS/VSCode and execute build commands there.
2. Run the console application by opening `Windows Terminal`, `Powershell`, `Command Prompt`, etc. in the directory where the `.csproj` file is located at `GhostConsole/GhostConsole` - the embedded `.gif` is using `Windows Terminal`. Alternatively, you can open the application in VS/VSCode and run the project from there.