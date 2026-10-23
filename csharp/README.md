# Search.FWK (C#)

AI search framework with sample problems, targeting .NET 8.

## Projects

| Project | Purpose |
|---|---|
| `Search.Backend` | Search algorithms (`UninformedSearch`, `InformedSearch`) and the `State` base class |
| `Search.Common` | Problem states: Water, Boat, Puzzle8, Sudoku |
| `Search.Console` | Console app (`Program.cs`) that runs the searches |

## Requirements

[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (`winget install Microsoft.DotNet.SDK.8`).

## Build

```powershell
dotnet build Search.FWK.sln
```

## Run

```powershell
dotnet run --project Search.Console
```

Every `Solve...` call in `Main` is commented out by default, so the program prints nothing until you enable one:

1. Open `Search.Console/Program.cs`.
2. Uncomment a line in `Main`, e.g. in the Puzzle8 region remove the `//` before `SolveBFS(initialPuzzle8State);`.
3. Run again.

Available methods: `SolveBFS`, `SolveDFS`, `SolveDLS`, `SolveIDS` (and `SolveGreedy`, used in the Puzzle8 region).
Available problems: Water, Boat, Puzzle8, Sudoku.

Each solve waits for Enter before continuing.

You can also open `Search.FWK.sln` in Visual Studio 2022+ and press F5 (after uncommenting a solve line).
