using System.Collections.Generic;

namespace Search.Backend
{
    public abstract class State
    {
        public abstract int GetDepth();
        public abstract int GetHeuristic();
        public abstract string GetPath();
        public abstract List<State> GetSuccessors();
        public abstract bool IsFinal();
    }
}
