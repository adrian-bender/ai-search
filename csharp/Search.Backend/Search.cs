using System.Collections.Generic;

namespace Search.Backend
{
    public class Search
    {
        #region Variables
        private string _finalState;
        private string _solutionPath;
        #endregion

        #region Constructors
        public Search()
        {
            _solutionPath = string.Empty;
            _finalState = string.Empty;
        }
        #endregion

        #region Public Methods
        public string GetFinalState()
        {
            return (_finalState);
        }

        public string GetSolutionPath()
        {
            return (_solutionPath);
        }

        // Busca el primer estado final de la colección y, si lo encuentra, guarda su camino
        protected bool TryFindSolution(IEnumerable<State> states)
        {
            foreach (State state in states)
            {
                if (state.IsFinal())
                {
                    _solutionPath = state.GetPath();
                    _finalState = state.ToString();
                    return (true);
                }
            }
            return (false);
        }

        public void InitializeSearch()
        {
            _solutionPath = string.Empty;
            _finalState = string.Empty;
        }
        #endregion
    }
}
