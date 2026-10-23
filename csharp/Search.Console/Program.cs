using System;
using System.Collections.Generic;
using System.Linq;
using Search.Backend;
using Search.Common;

namespace Search.Console
{
    class Program
    {
        #region Solve Methods
        static void ShowResult(string algorithm, bool hasSolution, string solutionPath)
        {
            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por {0} es:\n{1}", algorithm, solutionPath));
            else
                System.Console.WriteLine(string.Format("{0} no encontró solución al problema.", algorithm));
        }

        static void SolveBFS(State initialState)
        {
            UninformedSearch search = new UninformedSearch();
            ShowResult("BFS", search.SearchBFS(initialState), search.GetSolutionPath());
        }

        static void SolveDFS(State initialState)
        {
            UninformedSearch search = new UninformedSearch();
            ShowResult("DFS", search.SearchDFS(initialState), search.GetSolutionPath());
        }

        static void SolveDLS(State initialState, int maxDepth)
        {
            UninformedSearch search = new UninformedSearch();
            ShowResult(string.Format("DLS con tope de profundidad {0}", maxDepth), search.SearchDLS(initialState, maxDepth), search.GetSolutionPath());
        }

        static void SolveIDS(State initialState)
        {
            UninformedSearch search = new UninformedSearch();
            ShowResult("IDS", search.SearchIDS(initialState), search.GetSolutionPath());
        }

        static void SolveGreedy(State initialState)
        {
            InformedSearch search = new InformedSearch();
            ShowResult("Greedy", search.SearchGreedy(initialState), search.GetSolutionPath());
        }
        #endregion

        static State BuildPuzzle8(string board)
        {
            // Default board is the sample one; "--board 123456078" overrides it (0 = blank).
            if (board.Length != 9 || board.Any(c => !char.IsDigit(c)) || board.Distinct().Count() != 9)
                throw new ArgumentException("--board needs the digits 0-8 exactly once, e.g. 123456078");
            int[,] matrix = new int[3, 3];
            for (int i = 0; i < 9; i++)
                matrix[i / 3, i % 3] = board[i] - '0';
            return new Puzzle8State(matrix);
        }

        const string DefaultPuzzle8Board = "123456078";
        const string DefaultSudokuBoard = "001078050782009400500100000008700060307801204010003500000002005005600942040380600";

        static State BuildSudoku(string board)
        {
            // 81 digits row by row, 0 = empty cell.
            if (board.Length != 81 || board.Any(c => !char.IsDigit(c)))
                throw new ArgumentException("--board needs 81 digits (0 = empty), row by row");
            int[,,,] matrixSudoku = new int[3, 3, 3, 3];
            for (int i = 0; i < 81; i++)
            {
                int row = i / 9, col = i % 9;
                matrixSudoku[row / 3, col / 3, row % 3, col % 3] = board[i] - '0';
            }
            return new SudokuState(matrixSudoku);
        }

        static void PrintUsage()
        {
            System.Console.WriteLine("Usage: Search.Console --problem <water|boat|puzzle8|sudoku> --algorithm <bfs|dfs|dls|ids|greedy> [--depth N] [--board <digits>]");
            System.Console.WriteLine("  --depth  depth limit for dls (default 10)");
            System.Console.WriteLine("  --board  start board for puzzle8 (9 digits) or sudoku (81 digits), row by row, 0 = empty");
        }

        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            Dictionary<string, string> options = new Dictionary<string, string>();
            for (int i = 0; i + 1 < args.Length; i += 2)
                options[args[i].TrimStart('-').ToLowerInvariant()] = args[i + 1];

            if (!options.ContainsKey("problem") || !options.ContainsKey("algorithm"))
            {
                PrintUsage();
                return;
            }

            State initialState;
            switch (options["problem"].ToLowerInvariant())
            {
                case "water": initialState = new WaterState(0, 0); break;
                case "boat": initialState = new BoatState(false, false, false, false); break;
                case "puzzle8": initialState = BuildPuzzle8(options.GetValueOrDefault("board", DefaultPuzzle8Board)); break;
                case "sudoku": initialState = BuildSudoku(options.GetValueOrDefault("board", DefaultSudokuBoard)); break;
                default: PrintUsage(); return;
            }

            switch (options["algorithm"].ToLowerInvariant())
            {
                case "bfs": SolveBFS(initialState); break;
                case "dfs": SolveDFS(initialState); break;
                case "dls": SolveDLS(initialState, int.Parse(options.GetValueOrDefault("depth", "10"))); break;
                case "ids": SolveIDS(initialState); break;
                case "greedy": SolveGreedy(initialState); break;
                default: PrintUsage(); break;
            }
        }
    }
}
