using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Common
{
    public class SudokuState : State
    {
        #region Variables
        private int[,,,] _matrix;
        private string _path;
        #endregion

        #region Constructors
        public SudokuState(int[,,,] matrix)
        {
            _matrix = matrix;
            _path = string.Empty;
        }

        public SudokuState(int[, , ,] matrix, string path)
        {
            _matrix = matrix;
            _path = path;
        }
        #endregion

        #region Private Methods
        public bool AvailableInColumn(int number, int col, int colMatrix)
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
                for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                    if (_matrix[row, col, rowMatrix, colMatrix] == number)
                        return (false);
            return (true);
        }

        public bool AvailableInMatrix(int number, int row, int col)
        {
            for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                    if (_matrix[row, col, rowMatrix, colMatrix] == number)
                        return (false);
            return (true);
        }

        public bool AvailableInRow(int number, int row, int rowMatrix)
        {
            for (int col = 0; col < _matrix.GetLength(1); col++)
                for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                    if (_matrix[row, col, rowMatrix, colMatrix] == number)
                        return (false);
            return (true);
        }

        // Números que se pueden colocar en la celda sin repetirse en su matriz, fila ni columna
        private List<int> GetCandidates(int row, int col, int rowMatrix, int colMatrix)
        {
            List<int> output;
            output = new List<int>();
            for (int number = 1; number <= _matrix.GetLength(2) * _matrix.GetLength(3); number++)
                if (AvailableInMatrix(number, row, col) && AvailableInRow(number, row, rowMatrix) && AvailableInColumn(number, col, colMatrix))
                    output.Add(number);
            return (output);
        }
        #endregion

        #region Public Methods
        public override int GetDepth()
        {
            return (string.IsNullOrEmpty(_path) ? 0 : _path.Count(c => c == '|'));
        }

        public override int GetHeuristic()
        {
            return (0);
        }

        public override string GetPath()
        {
            return ((string.IsNullOrEmpty(_path)) ? string.Empty : (_path.Replace("|", "\n")));
        }

        public override List<State> GetSuccessors()
        {
            List<State> output;
            List<int> candidates;
            List<int> bestCandidates;
            int bestRow, bestCol, bestRowMatrix, bestColMatrix;
            output = new List<State>();
            bestCandidates = null;
            bestRow = 0; bestCol = 0; bestRowMatrix = 0; bestColMatrix = 0;
            // Elige la celda vacía con menos números posibles
            for (int row = 0; row < _matrix.GetLength(0); row++)
                for (int col = 0; col < _matrix.GetLength(1); col++)
                    for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                        for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                            if (_matrix[row, col, rowMatrix, colMatrix] == 0)
                            {
                                candidates = GetCandidates(row, col, rowMatrix, colMatrix);
                                if ((bestCandidates == null) || (candidates.Count < bestCandidates.Count))
                                {
                                    bestCandidates = candidates;
                                    bestRow = row; bestCol = col; bestRowMatrix = rowMatrix; bestColMatrix = colMatrix;
                                }
                            }
            // Genera un sucesor por cada número posible de esa celda
            if (bestCandidates != null)
                foreach (int number in bestCandidates)
                {
                    int[, , ,] matrix;
                    matrix = (int[, , ,])_matrix.Clone();
                    matrix[bestRow, bestCol, bestRowMatrix, bestColMatrix] = number;
                    output.Add(new SudokuState(matrix, _path + "|" + string.Format("Colocar {0} en la coordenada {1}, {2}.", number, bestRow * _matrix.GetLength(2) + bestRowMatrix + 1, bestCol * _matrix.GetLength(3) + bestColMatrix + 1)));
                }
            return (output);
        }

        public override bool IsFinal()
        {
            for (int row = 0; row < _matrix.GetLength(0); row++)
                for (int col = 0; col < _matrix.GetLength(1); col++)
                    for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                        for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                            if (_matrix[row, col, rowMatrix, colMatrix] == 0)
                                return (false);
            return (true);
        }

        public override string ToString()
        {
            string output;
            output = string.Empty;
            for (int row = 0; row < _matrix.GetLength(0); row++)
            {
                if (row > 0)
                {
                    for (int col = 0; col < _matrix.GetLength(1); col++)
                    {
                        if (col > 0)
                            output += "-|-";
                        for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(3); rowMatrix++)
                        {
                            if (rowMatrix > 0)
                                output += "-";
                            output += "-";
                        }
                    }
                    output += "\n";
                }
                for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                {
                    for (int col = 0; col < _matrix.GetLength(1); col++)
                    {
                        if (col > 0)
                            output += " | ";
                        for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                        {
                            if (colMatrix > 0)
                                output += " ";
                            output += ((_matrix[row, col, rowMatrix, colMatrix] == 0) ? " " : _matrix[row, col, rowMatrix, colMatrix].ToString());
                        }
                    }
                    output += "\n";
                }
            }
            return (output);
        }
        #endregion
    }
}
