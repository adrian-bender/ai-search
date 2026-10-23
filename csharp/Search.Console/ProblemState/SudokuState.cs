using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Console.ProblemState
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
        #endregion

        #region Public Methods
        public override int GetDepth()
        {
            return (((string.IsNullOrEmpty(_path)) || (_path.IndexOf("|") < 0)) ? 0 : _path.Split('|').Count());
        }

        public override string GetPath()
        {
            return ((string.IsNullOrEmpty(_path)) ? string.Empty : (_path.Replace("|", "\n")));
       }

        public override List<State> GetSucessors()
        {
            List<State> output;

            output = new List<State>();

            for (int number = 1; number < 10; number++)
            { 
                for (int row = 0; row < _matrix.GetLength(0); row++)
                    for (int col = 0; col < _matrix.GetLength(1); col++)
                    {
                        if (AvailableInMatrix(number, row, col))
                        {
                            for (int rowMatrix = 0; rowMatrix < _matrix.GetLength(2); rowMatrix++)
                            {
                                if (AvailableInRow(number, row, rowMatrix))
                                {
                                    for (int colMatrix = 0; colMatrix < _matrix.GetLength(3); colMatrix++)
                                    {
                                        if (AvailableInColumn(number, col, colMatrix))
                                        {
                                            int[, , ,] matrix;
                                            matrix = (int[, , ,])_matrix.Clone();

                                            matrix[row, col, rowMatrix, colMatrix] = number;

                                            output.Add(new SudokuState(matrix, _path + "|" + string.Format ("Colocar {0} en la coordenada {1}, {2}.", number, row + rowMatrix + 1, col + colMatrix + 1)));
                                        }
                                    }
                                }
                            }
                        }
                    }
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
