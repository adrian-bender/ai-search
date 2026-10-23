# Search.FWK (C#)

AI search framework with sample problems, targeting .NET 8.

## Projects

| Project | Purpose |
|---|---|
| `Search.Backend` | Search algorithms (`UninformedSearch`, `InformedSearch`) and the `State` base class |
| `Search.Common` | Problem states: Water, Boat, Puzzle8, Sudoku |
| `Search.Console` | Console app (`Program.cs`) that runs the searches |
| `Search.Tests` | xUnit tests for the states and the search algorithms |

## Requirements

[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (`winget install Microsoft.DotNet.SDK.8`).

## Build

```powershell
dotnet build Search.FWK.sln
```

## Test

```powershell
dotnet test Search.FWK.sln
```

## Run

```powershell
dotnet run --project Search.Console -- --problem <problem> --algorithm <algorithm> [options]
```

Pick the problem and algorithm with arguments; there is no need to edit `Program.cs` or rebuild between runs. Running with no arguments prints the usage.

| Argument | Values |
|---|---|
| `--problem` | `water`, `boat`, `puzzle8`, `sudoku` |
| `--algorithm` | `bfs`, `dfs`, `dls`, `ids`, `greedy` |
| `--depth N` | Depth limit for `dls` (default 10) |
| `--board` | Start board, digits row by row, 0 = empty. `puzzle8`: 9 digits (default `123456078`). `sudoku`: 81 digits (default: the sample board) |

Examples:

```powershell
dotnet run --project Search.Console -- --problem puzzle8 --algorithm bfs
dotnet run --project Search.Console -- --problem puzzle8 --algorithm greedy --board 123405678
dotnet run --project Search.Console -- --problem sudoku --algorithm dfs --board 001078050782009400500100000008700060307801204010003500000002005005600942040380600
dotnet run --project Search.Console -- --problem water --algorithm dls --depth 8
dotnet run --project Search.Console -- --problem boat --algorithm ids
```

After changing code, add `--no-build` only if you already built; otherwise `dotnet run` rebuilds automatically.
In Visual Studio, put the arguments in the Search.Console project properties > Debug > Launch profiles.

## Notes

These are tree searches: they do not keep a visited set, so the same state can be reached many times. This is intentional, to show how each algorithm behaves.

- BFS, DLS and IDS find a shortest solution on Water, Boat and Puzzle8.
- DFS (and Greedy, on some starting positions) can run forever on Water, Boat and Puzzle8, because the state graphs have cycles.
- For Sudoku every move fills a cell, so there are no cycles. DFS solves the sample board almost instantly.
- Greedy uses a heuristic only for Puzzle8 (misplaced tiles). On the other problems the heuristic is 0, so it behaves like BFS.

You can also open `Search.FWK.sln` in Visual Studio 2022+ and press F5 (set the arguments in the launch profile first).
