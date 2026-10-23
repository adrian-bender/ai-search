using System.Collections.Generic;

namespace Search.Backend
{
    public class UninformedSearch : Search
    {
        #region Constructors
        public UninformedSearch() : base()
        {

        }
        #endregion

        #region Public Methods
        public bool SearchBFS(State initialState)
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

                current = null;
            }

            output = GetSolutionPathFromNodes(nodes);

            nodes = null;

            return (output);
        }

        public bool SearchDFS(State initialState)
        {
            bool output;
            List<State> nodes;

            output = false;

            initializeSearch();

            nodes = new List<State>();

            // Agrega raíz a la lista
            nodes.Add(initialState);

            while ((nodes.Count > 0) && (!GetSolutionPathFromNodes(nodes)))
            {
                State current;

                current = nodes[0];

                // Remueve el primer elemento
                nodes.RemoveAt(0);

                // Agrega sucesores al inicio de la lista
                nodes.InsertRange(0, current.GetSucessors());

                current = null;
            }

            output = GetSolutionPathFromNodes(nodes);

            nodes = null;

            return (output);
        }

        public bool SearchDLS(State initialState, int maxDepth)
        {
            bool output;
            List<State> nodes;

            output = false;

            initializeSearch();

            nodes = new List<State>();

            // Agrega raíz a la lista
            nodes.Add(initialState);

            while ((nodes.Count > 0) && (!GetSolutionPathFromNodes(nodes)))
            {
                State current;

                current = nodes[0];

                // Remueve el primer elemento
                nodes.RemoveAt(0);

                if (current.GetDepth () < maxDepth)
                // Agrega sucesores al inicio de la lista
                    nodes.InsertRange(0, current.GetSucessors());

                current = null;
            }

            output = GetSolutionPathFromNodes(nodes);

            nodes = null;

            return (output);
        }

        public bool SearchIDS(State initialState)
        {
            bool output;
            int maxDepth;

            output = false;
            maxDepth = 0;

            while (!output) {
                output = SearchDLS(initialState, maxDepth);
                maxDepth++;
            }

            return (output);
        }
        #endregion
    }
}
