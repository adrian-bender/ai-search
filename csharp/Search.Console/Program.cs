using Search.Backend;
using Search.Console.ProblemState;

namespace Search.Console
{
    class Program
    {
        #region Solve Methods
        static void SolveBFS(State initialState)
        {
            bool hasSolution;
            UninformedSearch uninformedSearch;

            uninformedSearch = new UninformedSearch();

            hasSolution = uninformedSearch.SearchBFS(initialState);

            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por BFS es:\n{0}", uninformedSearch.GetSolutionPath()));
            else
                System.Console.WriteLine("BFS no encontró solución al problema.");

            System.Console.ReadLine();

            uninformedSearch = null;
        }

        static void SolveDFS(State initialState)
        {
            bool hasSolution;
            UninformedSearch uninformedSearch;

            uninformedSearch = new UninformedSearch();

            hasSolution = uninformedSearch.SearchDFS(initialState);

            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por DFS es:\n{0}", uninformedSearch.GetSolutionPath()));
            else
                System.Console.WriteLine("DFS no encontró solución al problema.");
            
            System.Console.ReadLine();

            uninformedSearch = null;
        }

        static void SolveDLS(State initialState, int maxDepth)
        {
            bool hasSolution;
            UninformedSearch uninformedSearch;

            uninformedSearch = new UninformedSearch();

            hasSolution = uninformedSearch.SearchDLS (initialState, maxDepth);

            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por DLS con tope de profundidad {1} es:\n{0}", uninformedSearch.GetSolutionPath(), maxDepth));
            else
                System.Console.WriteLine(string.Format("DLS con tope de profundidad {0} no encontró solución al problema.", maxDepth));

            System.Console.ReadLine();

            uninformedSearch = null;
        }

        static void SolveIDS(State initialState)
        {
            bool hasSolution;
            UninformedSearch uninformedSearch;

            uninformedSearch = new UninformedSearch();

            hasSolution = uninformedSearch.SearchIDS(initialState);

            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por IDS es:\n{0}", uninformedSearch.GetSolutionPath()));
            else
                System.Console.WriteLine("IDS no encontró solución al problema.");
            System.Console.ReadLine();

            uninformedSearch = null;
        }

        static void SolveGreedy(State initialState)
        {
            bool hasSolution;
            InformedSearch informedSearch;

            informedSearch = new InformedSearch();

            hasSolution = informedSearch.SearchGreedy(initialState);

            if (hasSolution)
                System.Console.WriteLine(string.Format("La solución al problema encontrada por Greedy es:\n{0}", informedSearch.GetSolutionPath()));
            else
                System.Console.WriteLine("Greedy no encontró solución al problema.");
            System.Console.ReadLine();

            informedSearch = null;
        }
        #endregion

        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            #region Solve Water Problem
            WaterState initialWaterState;
            initialWaterState = new WaterState(0, 0);
            //SolveBFS(initialWaterState);
            //SolveDFS(initialWaterState);
            //SolveDLS(initialWaterState, 10);
            //SolveIDS(initialWaterState);
            initialWaterState = null;
            #endregion

            #region Solve Boat Problem
/*
A farmer wants to cross a river and take with him a wolf, a goat, and a cabbage. 
There is a boat that can fit himself plus either the wolf, the goat, or the cabbage. 
If the wolf and the goat are alone on one shore, the wolf will eat the goat. If the goat and the cabbage are alone on the shore, the goat will eat the cabbage.
How can the farmer bring the wolf, the goat, and the cabbage across the river?
*/
            BoatState initialBoatState;
            initialBoatState = new BoatState(false, false, false, false);
            //SolveBFS(initialBoatState);
            //SolveDFS(initialBoatState);
            //SolveDLS(initialBoatState, 10);
            //SolveIDS(initialBoatState);
            initialBoatState = null;
            #endregion

            #region Solve Puzzle8 Problem
            #region One Puzzle8 matrix Definition
            int[,] matrixPuzzle8;
            matrixPuzzle8 = new int[3, 3];
            matrixPuzzle8[0, 0] = 1;
            matrixPuzzle8[0, 1] = 2;
            matrixPuzzle8[0, 2] = 3;
            matrixPuzzle8[1, 0] = 4;
            matrixPuzzle8[1, 1] = 5;
            matrixPuzzle8[1, 2] = 6;
            matrixPuzzle8[2, 0] = 0;
            matrixPuzzle8[2, 1] = 7;
            matrixPuzzle8[2, 2] = 8;
            #endregion
            Puzzle8State initialPuzzle8State;
            initialPuzzle8State = new Puzzle8State(matrixPuzzle8);
            //SolveBFS(initialPuzzle8State);
            //SolveDFS(initialPuzzle8State);
            //SolveDLS(initialPuzzle8State, 10);
            //SolveIDS(initialPuzzle8State);
            //SolveGreedy(initialPuzzle8State);
            initialPuzzle8State = null;
            #endregion

            #region Solve Sudoku Problem
            #region One Sudoku matrix Definition
            int[, , ,] matrixSudoku;
            matrixSudoku = new int[3, 3, 3, 3];
            matrixSudoku[0,0,0,2] = 1;
            matrixSudoku[0,0,1,0] = 7;
            matrixSudoku[0,0,1,1] = 8;
            matrixSudoku[0,0,1,2] = 2;
            matrixSudoku[0,0,2,0] = 5;
            matrixSudoku[0,1,0,1] = 7;
            matrixSudoku[0,1,0,2] = 8;
            matrixSudoku[0,1,1,2] = 9;
            matrixSudoku[0,1,2,0] = 1;
            matrixSudoku[0,2,0,1] = 5;
            matrixSudoku[0,2,1,0] = 4;
            matrixSudoku[1,0,0,2] = 8;
            matrixSudoku[1,0,1,0] = 3;
            matrixSudoku[1,0,1,2] = 7;
            matrixSudoku[1,0,2,1] = 1;
            matrixSudoku[1,1,0,0] = 7;
            matrixSudoku[1,1,1,0] = 8;
            matrixSudoku[1,1,1,2] = 1;
            matrixSudoku[1,1,2,2] = 3;
            matrixSudoku[1,2,0,1] = 6;
            matrixSudoku[1,2,1,0] = 2;
            matrixSudoku[1,2,1,2] = 4;
            matrixSudoku[1,2,2,0] = 5;
            matrixSudoku[2,0,1,2] = 5;
            matrixSudoku[2,0,2,1] = 4;
            matrixSudoku[2,1,0,2] = 2;
            matrixSudoku[2,1,1,0] = 6;
            matrixSudoku[2,1,2,0] = 3;
            matrixSudoku[2,1,2,1] = 8;
            matrixSudoku[2,2,0,2] = 5;
            matrixSudoku[2,2,1,0] = 9;
            matrixSudoku[2,2,1,1] = 4;
            matrixSudoku[2,2,1,2] = 2;
            matrixSudoku[2,2,2,0] = 6;
            #endregion

            SudokuState initialSudokuState;
            initialSudokuState = new SudokuState(matrixSudoku, "");
            //SolveBFS(initialWaterState);
            //SolveDFS(initialWaterState);
            //SolveDLS(initialWaterState, 10);
            //SolveIDS(initialSudokuState);
            initialSudokuState = null;
            #endregion
        }
    }
}
