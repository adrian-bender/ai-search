using System.Collections.Generic;
using System.Linq;

namespace Search.Backend
{
    public class InformedSearch : Search
    {
        #region Constructors
        public InformedSearch()
            : base()
        {
        }
        #endregion

        #region Public Methods
        public bool SearchGreedy(State initialState)
        {
            bool output;
            long order;
            PriorityQueue<State, (int, long)> nodes;
            InitializeSearch();
            order = 0;
            nodes = new PriorityQueue<State, (int, long)>();
        // Agrega raíz a la cola. El contador desempata por orden de llegada, como un ordenamiento estable
            nodes.Enqueue(initialState, (initialState.GetHeuristic(), order++));
            output = TryFindSolution(new List<State> { initialState });
            while ((nodes.Count > 0) && (!output))
            {
                State current;
                List<State> successors;
            // Remueve el elemento con menor heurística
                current = nodes.Dequeue();
                successors = current.GetSuccessors();
                output = TryFindSolution(successors.OrderBy(o => o.GetHeuristic()).ToList());
            // Agrega sucesores a la cola
                foreach (State successor in successors)
                    nodes.Enqueue(successor, (successor.GetHeuristic(), order++));
            }
            return (output);
        }
        #endregion
    }
}
