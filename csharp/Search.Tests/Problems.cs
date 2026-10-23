using Search.Common;

namespace Search.Tests
{
    // Instancias de los problemas usadas por las pruebas
    static class Problems
    {
        public static WaterState Water()
        {
            return new WaterState(0, 0);
        }

        public static BoatState Boat()
        {
            return new BoatState(false, false, false, false);
        }

        // Faltan dos movimientos para resolverlo
        public static Puzzle8State Puzzle8()
        {
            return new Puzzle8State(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 0, 7, 8 } });
        }

        public static int[,,,] SudokuGivens()
        {
            int[,,,] m = new int[3, 3, 3, 3];
            m[0,0,0,2] = 1;
            m[0,0,1,0] = 7;
            m[0,0,1,1] = 8;
            m[0,0,1,2] = 2;
            m[0,0,2,0] = 5;
            m[0,1,0,1] = 7;
            m[0,1,0,2] = 8;
            m[0,1,1,2] = 9;
            m[0,1,2,0] = 1;
            m[0,2,0,1] = 5;
            m[0,2,1,0] = 4;
            m[1,0,0,2] = 8;
            m[1,0,1,0] = 3;
            m[1,0,1,2] = 7;
            m[1,0,2,1] = 1;
            m[1,1,0,0] = 7;
            m[1,1,1,0] = 8;
            m[1,1,1,2] = 1;
            m[1,1,2,2] = 3;
            m[1,2,0,1] = 6;
            m[1,2,1,0] = 2;
            m[1,2,1,2] = 4;
            m[1,2,2,0] = 5;
            m[2,0,1,2] = 5;
            m[2,0,2,1] = 4;
            m[2,1,0,2] = 2;
            m[2,1,1,0] = 6;
            m[2,1,2,0] = 3;
            m[2,1,2,1] = 8;
            m[2,2,0,2] = 5;
            m[2,2,1,0] = 9;
            m[2,2,1,1] = 4;
            m[2,2,1,2] = 2;
            m[2,2,2,0] = 6;
            return m;
        }

        public static SudokuState Sudoku()
        {
            return new SudokuState(SudokuGivens());
        }

        // Tablero resuelto de 9x9 (fila, columna) tomado como solución conocida
        public static readonly int[,] SolvedSudoku = new int[,]
        {
            { 4, 6, 1, 2, 7, 8, 3, 5, 9 },
            { 7, 8, 2, 5, 3, 9, 4, 1, 6 },
            { 5, 9, 3, 1, 4, 6, 7, 2, 8 },
            { 9, 2, 8, 7, 5, 4, 1, 6, 3 },
            { 3, 5, 7, 8, 6, 1, 2, 9, 4 },
            { 6, 1, 4, 9, 2, 3, 5, 8, 7 },
            { 1, 7, 6, 4, 9, 2, 8, 3, 5 },
            { 8, 3, 5, 6, 1, 7, 9, 4, 2 },
            { 2, 4, 9, 3, 8, 5, 6, 7, 1 }
        };

        // Sudoku resuelto salvo por la celda (fila, columna) de 0 a 8
        public static SudokuState SudokuMissingCell(int row, int col)
        {
            int[,,,] m = new int[3, 3, 3, 3];
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    if ((r != row) || (c != col))
                        m[r / 3, c / 3, r % 3, c % 3] = SolvedSudoku[r, c];
            return new SudokuState(m);
        }
    }
}
