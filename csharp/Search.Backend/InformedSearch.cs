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
            List<State> nodes;

            output = false;

            initializeSearch();

            nodes = new List<State>();

        // Agrega raíz a la lista
            nodes.Add(initialState);

            while ((nodes.Count > 0) && (!GetSolutionPathFromNodes (nodes))) {
                State current;

                current = nodes[0];

            // Remueve el primer elemento
                nodes.RemoveAt(0);

            // Agrega sucesores al final de la lista
                nodes.AddRange(current.GetSucessors());

                nodes = nodes.OrderBy(o => o.GetHeuristic()).ToList();

                current = null;
            }

            output = GetSolutionPathFromNodes(nodes);

            nodes = null;

            return (output);
        }
        #endregion
    }
}
