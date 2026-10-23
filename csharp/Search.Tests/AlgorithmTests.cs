using System.Linq;
using Search.Backend;
using Search.Common;
using Xunit;

namespace Search.Tests
{
    public class AlgorithmTests
    {
        private static int Moves(Backend.Search search)
        {
            return search.GetSolutionPath().Split('\n', System.StringSplitOptions.RemoveEmptyEntries).Length;
        }

        // Dígitos del tablero impreso por SudokuState.ToString(), en orden de fila
        private static int[,] Grid(string text)
        {
            int[] digits = text.Where(char.IsDigit).Select(c => c - '0').ToArray();
            Assert.Equal(81, digits.Length);
            int[,] grid = new int[9, 9];
            for (int i = 0; i < 81; i++)
                grid[i / 9, i % 9] = digits[i];
            return grid;
        }

        private static void AssertValidSudoku(int[,] grid)
        {
            for (int i = 0; i < 9; i++)
            {
                Assert.Equal(Enumerable.Range(1, 9), Enumerable.Range(0, 9).Select(k => grid[i, k]).OrderBy(x => x));
                Assert.Equal(Enumerable.Range(1, 9), Enumerable.Range(0, 9).Select(k => grid[k, i]).OrderBy(x => x));
                Assert.Equal(Enumerable.Range(1, 9), Enumerable.Range(0, 9).Select(k => grid[(i / 3) * 3 + k / 3, (i % 3) * 3 + k % 3]).OrderBy(x => x));
            }
        }

        #region BFS
        [Fact]
        public void BFS_Water_EncuentraLaSolucionMasCortaDeSeisMovimientos()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(Problems.Water()));
            Assert.Equal(6, Moves(search));
            Assert.Equal("Jarra de 4: 2, Jarra de 3: 3", search.GetFinalState());
        }

        [Fact]
        public void BFS_Boat_EncuentraLaSolucionMasCortaDeSieteMovimientos()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(Problems.Boat()));
            Assert.Equal(7, Moves(search));
            Assert.Equal("Orilla inicial: \nOrilla final: Granjero, Lobo, Cabra, Repollo", search.GetFinalState());
        }

        [Fact]
        public void BFS_Puzzle8_EncuentraLaSolucionDeDosMovimientos()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(Problems.Puzzle8()));
            Assert.Equal(2, Moves(search));
        }

        [Fact]
        public void BFS_SiElEstadoInicialYaEsFinalNoHaceFaltaMoverse()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(new Puzzle8State(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } })));
            Assert.Equal(string.Empty, search.GetSolutionPath());
        }
        #endregion

        #region DFS
        [Fact]
        public void DFS_Sudoku_ResuelveElTableroRespetandoLosNumerosDados()
        {
            UninformedSearch search = new UninformedSearch();
            int[,,,] givens = Problems.SudokuGivens();
            Assert.True(search.SearchDFS(Problems.Sudoku()));
            int[,] grid = Grid(search.GetFinalState());
            AssertValidSudoku(grid);
            for (int r = 0; r < 9; r++)
                for (int c = 0; c < 9; c++)
                    if (givens[r / 3, c / 3, r % 3, c % 3] != 0)
                        Assert.Equal(givens[r / 3, c / 3, r % 3, c % 3], grid[r, c]);
        }

        [Fact]
        public void BFS_Sudoku_ResuelveElTablero()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(Problems.Sudoku()));
            AssertValidSudoku(Grid(search.GetFinalState()));
        }
        #endregion

        #region DLS
        [Fact]
        public void DLS_RespetaExactamenteElTopeDeProfundidad()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.False(search.SearchDLS(Problems.Boat(), 6));
            Assert.True(search.SearchDLS(Problems.Boat(), 7));
            Assert.False(search.SearchDLS(Problems.Puzzle8(), 1));
            Assert.True(search.SearchDLS(Problems.Puzzle8(), 2));
        }

        [Fact]
        public void DLS_ConTopeCeroSoloRevisaLaRaiz()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.False(search.SearchDLS(Problems.Water(), 0));
        }
        #endregion

        #region IDS
        [Fact]
        public void IDS_EncuentraLaSolucionMasCortaEnCadaProblema()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchIDS(Problems.Water()));
            Assert.Equal(6, Moves(search));
            Assert.True(search.SearchIDS(Problems.Boat()));
            Assert.Equal(7, Moves(search));
            Assert.True(search.SearchIDS(Problems.Puzzle8()));
            Assert.Equal(2, Moves(search));
        }
        #endregion

        #region Greedy
        [Fact]
        public void Greedy_Puzzle8_EncuentraLaSolucion()
        {
            InformedSearch search = new InformedSearch();
            Assert.True(search.SearchGreedy(Problems.Puzzle8()));
            Assert.Equal(2, Moves(search));
        }

        [Fact]
        public void Greedy_ConHeuristicaCeroSeComportaComoBFS()
        {
            InformedSearch greedy = new InformedSearch();
            UninformedSearch bfs = new UninformedSearch();
            Assert.True(greedy.SearchGreedy(Problems.Water()));
            Assert.True(bfs.SearchBFS(Problems.Water()));
            Assert.Equal(bfs.GetSolutionPath(), greedy.GetSolutionPath());
        }
        #endregion
    }
}
