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

        public bool GetSolutionPathFromNodes(List<State> nodes)
        {
            bool output;

            output = false;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].IsFinal())
                {
                    output = true;
                    _solutionPath = nodes[i].GetPath();
                    _finalState = nodes[i].ToString();
                    break;
                }
            }

            return (output);
        }

        public void initializeSearch()
        {
            _solutionPath = string.Empty;
            _finalState = string.Empty;
        }
        #endregion
    }
}
