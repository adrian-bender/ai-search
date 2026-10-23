using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Common
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
            output = new List<State>();
        // 1) Llena la jarra de 4
            if (_jar4 < 4)
                output.Add(new WaterState(4, _jar3, _path + "|Llenar la jarra de 4."));
        // 2) Llena la jarra de 3
            if (_jar3 < 3)
                output.Add(new WaterState(_jar4, 3, _path + "|Llenar la jarra de 3."));
        // 3) Vacía la jarra de 4
            if (_jar4 > 0)
                output.Add(new WaterState(0, _jar3, _path + "|Vaciar la jarra de 4."));
        // 4) Vacía la jarra de 3
            if (_jar3 > 0)
                output.Add(new WaterState(_jar4, 0, _path + "|Vaciar la jarra de 3."));
        // 5) Traspasa la jarra de 4 a la de 3 (hasta vaciar la de 4 o llenar la de 3)
            if ((_jar4 > 0) && (_jar3 < 3))
            {
                int moved = System.Math.Min(_jar4, 3 - _jar3);
                output.Add(new WaterState(_jar4 - moved, _jar3 + moved, _path + "|Traspasar la jarra de 4 a la de 3."));
            }
        // 6) Traspasa la jarra de 3 a la de 4 (hasta vaciar la de 3 o llenar la de 4)
            if ((_jar3 > 0) && (_jar4 < 4))
            {
                int moved = System.Math.Min(_jar3, 4 - _jar4);
                output.Add(new WaterState(_jar4 + moved, _jar3 - moved, _path + "|Traspasar la jarra de 3 a la de 4."));
            }
            return (output);
        }

        public override bool IsFinal()
        {
            return (_jar4 == 2);
        }

        public override string ToString()
        {
            return (string.Format("Jarra de 4: {0}, Jarra de 3: {1}", _jar4, _jar3));
        }
        #endregion
    }
}
