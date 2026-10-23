using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Search.Backend;
using Search.Common;
using Xunit;

namespace Search.Tests
{
    public class StateTests
    {
        private static int[] Jars(State state)
        {
            Match match = Regex.Match(state.ToString(), @"Jarra de 4: (\d+), Jarra de 3: (\d+)");
            return new int[] { int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value) };
        }

        [Fact]
        public void Water_NingunSucesorSuperaLaCapacidadDeLasJarras()
        {
            HashSet<string> seen = new HashSet<string>();
            Queue<State> pending = new Queue<State>();
            pending.Enqueue(Problems.Water());
            while (pending.Count > 0)
            {
                foreach (State successor in pending.Dequeue().GetSuccessors())
                {
                    int[] jars = Jars(successor);
                    Assert.InRange(jars[0], 0, 4);
                    Assert.InRange(jars[1], 0, 3);
                    if (seen.Add(successor.ToString()))
                        pending.Enqueue(successor);
                }
            }
            Assert.True(seen.Count > 1);
        }

        [Fact]
        public void Water_TraspasarLlenaLaJarraDestinoSinPasarse()
        {
            // (4,0): pasar de la jarra de 4 a la de 3 deja (1,3)
            State state = new WaterState(4, 0);
            Assert.Contains(state.GetSuccessors(), s => Jars(s).SequenceEqual(new int[] { 1, 3 }));
            Assert.DoesNotContain(state.GetSuccessors(), s => Jars(s)[1] > 3);
        }

        [Fact]
        public void GetDepth_LaRaizTieneProfundidadCeroYCadaMovimientoSumaUno()
        {
            State root = Problems.Boat();
            Assert.Equal(0, root.GetDepth());
            State child = root.GetSuccessors().First();
            Assert.Equal(1, child.GetDepth());
            Assert.Equal(2, child.GetSuccessors().First().GetDepth());
        }

        [Fact]
        public void Boat_BFSEncuentraLaSolucionDeSieteMovimientos()
        {
            UninformedSearch search = new UninformedSearch();
            Assert.True(search.SearchBFS(Problems.Boat()));
            Assert.Equal(7, search.GetSolutionPath().Split('\n', System.StringSplitOptions.RemoveEmptyEntries).Length);
        }

        [Fact]
        public void Sudoku_ElSucesorDeUnaUnicaCeldaVaciaIndicaSuCoordenadaCompleta()
        {
            // Celda vacía en la fila 8 y columna 6 (1 a 9): hay un solo número posible
            State state = Problems.SudokuMissingCell(7, 5);
            List<State> successors = state.GetSuccessors();
            Assert.Single(successors);
            Assert.Equal("\nColocar 7 en la coordenada 8, 6.", successors[0].GetPath());
            Assert.True(successors[0].IsFinal());
        }

        [Fact]
        public void Sudoku_UnaCeldaSinNumerosPosiblesNoGeneraSucesores()
        {
            // La celda (1,1) tiene del 1 al 8 en su fila y el 9 en su columna
            int[,,,] m = new int[3, 3, 3, 3];
            for (int c = 1; c < 9; c++)
                m[0, c / 3, 0, c % 3] = c;
            m[1, 0, 0, 0] = 9;
            Assert.Empty(new SudokuState(m).GetSuccessors());
        }
    }
}
