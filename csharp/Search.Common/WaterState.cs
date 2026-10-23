using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Console.ProblemState
{
    public class WaterState : State
    {
        #region Variables
        private int _jar4;
        private int _jar3;
        private string _path;
        #endregion

        #region Constructors
        public WaterState(int jar4, int jar3)
        {
            _jar4 = jar4;
            _jar3 = jar3;
            _path = string.Empty;
        }

        public WaterState(int jar4, int jar3, string path)
        {
            _jar4 = jar4;
            _jar3 = jar3;
            _path = path;
        }
        #endregion

        #region Public Methods
        public override int GetDepth()
        {
            return (((string.IsNullOrEmpty(_path)) || (_path.IndexOf("|") < 0)) ? 0 : _path.Split('|').Count());
        }

        public override int GetHeuristic()
        {
            return (0);
        }

        public override string GetPath()
        {
            return ((string.IsNullOrEmpty(_path)) ? string.Empty : (_path.Replace("|", "\n")));
        }

        public override List<State> GetSucessors()
        {
            List<State> output;

            output = new List<State>();

        // 1) Llena la jarra de 4
            if (_jar4 < 4)
                output.Add(new WaterState(4, _jar3, _path + "|LLenar la jarra de 4."));
        // 2) Llena la jarra de 3
            if (_jar3 < 3)
                output.Add(new WaterState(_jar4, 3, _path + "|Llenar la jarra de 3."));
        // 3) Vacía la jarra de 4
            if (_jar4 > 0)
                output.Add(new WaterState(0, _jar3, _path + "|Vaciar la jarra de 4."));
        // 4) Vacía la jarra de 3
            if (_jar3 > 0)
                output.Add(new WaterState(_jar4, 0, _path + "|Vaciar la jarra de 3."));
        // 5) Traspasa la jarra de 4 a la de 3
            if ((_jar4 > 0) && ((_jar4 + _jar3) <= 4))
                output.Add(new WaterState(0, _jar4 + _jar3, _path + "|Traspasar la jarra de 4 a la de 3."));
        // 6) Traspasa la jarra de 4 a la de 3
            if ((_jar3 > 0) && ((_jar4 + _jar3) <= 3))
                output.Add(new WaterState(_jar4 + _jar3, 0, _path + "|Traspasar la jarra de 3 a la de 4."));
        // 7) Vacía la jarra de 4 en la de 3
            if ((_jar4 > 0) && ((_jar4 + _jar3) > 4))
                output.Add(new WaterState(_jar4 - (3 - _jar3), 3, _path + "|Vaciar la jarra de 4 en la de 3."));
        // 8) Vacía la jarra de 3 en la de 4
            if ((_jar3 > 0) && ((_jar4 + _jar3) > 3))
                output.Add(new WaterState(4, _jar3 - (4 - _jar4), _path + "|Vaciar la jarra de 3 en la de 4."));

            return (output);
        }

        public override bool IsFinal()
        {
            return (_jar4 == 2);
        }
        #endregion
    }
}
