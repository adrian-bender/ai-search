using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Common
{
    public class Puzzle8State : State
    {
        #region Variables
        private int[,] _matrix;
        private string _path;
        #endregion

        #region Constructors
        public Puzzle8State(int[,] matrix)
        {
            _matrix = matrix;
            _path = string.Empty;
        }

        public Puzzle8State(int[,] matrix, string path)
        {
            _matrix = matrix;
            _path = path;
        }
        #endregion

        #region Private Methods
        private void GetBlankPosition (out int row, out int col)
        {
            row = 0; col = 0;
            for (row = 0; row < _matrix.GetLength(0); row++)
                for (col = 0; col < _matrix.GetLength(1); col++)
                    if (_matrix[row, col] == 0)
                        return;
        }
        #endregion

        #region Public Methods
        public override int GetDepth()
        {
            return (string.IsNullOrEmpty(_path) ? 0 : _path.Count(c => c == '|'));
        }

        public override int GetHeuristic()
        {
            int output = 0;
            if (_matrix[0, 0] != 1) output++;
            if (_matrix[0, 1] != 2) output++;
            if (_matrix[0, 2] != 3) output++;
            if (_matrix[1, 0] != 4) output++;
            if (_matrix[1, 1] != 5) output++;
            if (_matrix[1, 2] != 6) output++;
            if (_matrix[2, 0] != 7) output++;
            if (_matrix[2, 1] != 8) output++;
            return (output);
        }

        public override string GetPath()
        {
            return ((string.IsNullOrEmpty(_path)) ? string.Empty : (_path.Replace("|", "\n")));
        }

        public override List<State> GetSuccessors()
        {
            int row, col;
            List<State> output;
            output = new List<State>();
            GetBlankPosition (out row, out col);
            // 1) Mueve blanco arriba
            if (row > 0)
            {
                int[,] matrix;
                matrix = (int[,])_matrix.Clone();
                matrix[row, col] = matrix[row - 1, col];
                matrix[row - 1, col] = 0;
                output.Add(new Puzzle8State(matrix, _path + "|Mueve blanco arriba."));
            }
            // 2) Mueve blanco abajo
            if (row < 2)
            {
                int[,] matrix;
                matrix = (int[,])_matrix.Clone();
                matrix[row, col] = matrix[row + 1, col];
                matrix[row + 1, col] = 0;
                output.Add(new Puzzle8State(matrix, _path + "|Mueve blanco abajo."));
            }
            // 3) Mueve blanco a la derecha
            if (col < 2)
            {
                int[,] matrix;
                matrix = (int[,])_matrix.Clone();
                matrix[row, col] = matrix[row, col + 1];
                matrix[row, col + 1] = 0;
                output.Add(new Puzzle8State(matrix, _path + "|Mueve blanco a la derecha."));
            }
            // 4) Mueve blanco a la izquierda
            if (col > 0)
            {
                int[,] matrix;
                matrix = (int[,])_matrix.Clone();
                matrix[row, col] = matrix[row, col - 1];
                matrix[row, col - 1] = 0;
                output.Add(new Puzzle8State(matrix, _path + "|Mueve blanco a la izquierda."));
            }
            return (output);
        }

        public override bool IsFinal()
        {
            return ((_matrix[0, 0] == 1) && (_matrix[0, 1] == 2) && (_matrix[0, 2] == 3) &&
                (_matrix[1, 0] == 4) && (_matrix[1, 1] == 5) && (_matrix[1, 2] == 6) &&
                (_matrix[2, 0] == 7) && (_matrix[2, 1] == 8) && (_matrix[2, 2] == 0));
        }

        public override string ToString()
        {
            string output;
            output = string.Empty;
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                for (int col = 0; col < _matrix.GetLength(1); col++)
                {
                    if (col > 0)
                        output += " ";
                    output += ((_matrix[row, col] == 0) ? " " : _matrix[row, col].ToString());
                }
                output += "\n";
            }
            return (output);
        }
        #endregion
    }
}
