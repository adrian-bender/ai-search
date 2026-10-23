using System.Collections.Generic;
using System.Linq;
using Search.Backend;

namespace Search.Common
{
    public class BoatState : State
    {
        #region Variables
        private bool _farmer;
        private bool _wolf;
        private bool _goat;
        private bool _cabbage;
        private string _path;
        #endregion

        #region Constructors
        public BoatState(bool farmer, bool wolf, bool goat, bool cabbage)
        {
            _farmer = farmer;
            _wolf = wolf;
            _goat = goat;
            _cabbage = cabbage;
            _path = string.Empty;
        }

        public BoatState(bool farmer, bool wolf, bool goat, bool cabbage, string path)
        {
            _farmer = farmer;
            _wolf = wolf;
            _goat = goat;
            _cabbage = cabbage;
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
            /* 
             1) Pasa solo cuando:
             * No quedan el lobo y la cabra del otro lado
             * No quedan la cabra y el repollo del otro lado
            */
            if ((!((_farmer == _wolf) && (_wolf == _goat))) &&
                (!((_farmer == _goat) && (_goat == _cabbage))))
                output.Add(new BoatState(!_farmer, _wolf, _goat, _cabbage, _path + "|" + ((_farmer) ? "Vuelve solo." : "Pasa solo.")));
            /* 
             2) Pasa con el lobo cuando:
             * Están en el mismo lado
             * No deja a la cabra y al repollo juntos
            */
            if ((_farmer == _wolf) &&
                (!(_goat == _cabbage)))
                output.Add(new BoatState(!_farmer, !_wolf, _goat, _cabbage, _path + "|" + ((_farmer) ? "Vuelve con el lobo." : "Pasa con el lobo.")));
            /* 
             3) Pasa con la cabra cuando:
             * Están en el mismo lado
            */
            if ((_farmer == _goat))
                output.Add(new BoatState(!_farmer, _wolf, !_goat, _cabbage, _path + "|" + ((_farmer) ? "Vuelve con la cabra." : "Pasa con la cabra.")));
            /* 
             4) Pasa el repollo cuando:
             * Están en el mismo lado
             * No deja al lobo y a la cabra juntos
            */
            if ((_farmer == _cabbage) &&
                (!(_wolf == _goat)))
                output.Add(new BoatState(!_farmer, _wolf, _goat, !_cabbage, _path + "|" + ((_farmer) ? "Vuelve con el repollo." : "Pasa con el repollo.")));
            return (output);
        }

        public override bool IsFinal()
        {
            return (_farmer && _wolf && _goat && _cabbage);
        }

        public override string ToString()
        {
            string[] names = { "Granjero", "Lobo", "Cabra", "Repollo" };
            bool[] sides = { _farmer, _wolf, _goat, _cabbage };
            List<string> start = new List<string>();
            List<string> end = new List<string>();
            for (int i = 0; i < names.Length; i++)
                (sides[i] ? end : start).Add(names[i]);
            return (string.Format("Orilla inicial: {0}\nOrilla final: {1}", string.Join(", ", start), string.Join(", ", end)));
        }
        #endregion
    }
}
