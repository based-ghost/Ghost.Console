// Set the console output encoding to UTF8 (needed to render Spectre.Console spinners)
Console.OutputEncoding = Encoding.UTF8;

// Init setup & info messages
ConsoleHelper.WriteMainTitle();

// Check console supports interaction & user confirmed continue
if (!ConsoleHelper.EnvironmentIsVerified)
    return;

// Fetch unique albums & build selection list
// Fetch photos for selected album and display as JsonText
// TODO: Abstraction/refactoring for code in this method
await ConsoleHelper.ExecuteAlbumAndPhotoPrompts();