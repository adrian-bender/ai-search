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
            Queue<State> nodes;
            InitializeSearch();
            nodes = new Queue<State>();
        // Agrega raíz a la cola
            nodes.Enqueue(initialState);
            output = TryFindSolution(nodes);
            while ((nodes.Count > 0) && (!output))
            {
                State current;
                List<State> successors;
            // Remueve el primer elemento
                current = nodes.Dequeue();
                successors = current.GetSuccessors();
                output = TryFindSolution(successors);
            // Agrega sucesores al final de la cola
                foreach (State successor in successors)
                    nodes.Enqueue(successor);
            }
            return (output);
        }

        public bool SearchDFS(State initialState)
        {
            bool output;
            Stack<State> nodes;
            InitializeSearch();
            nodes = new Stack<State>();
            // Agrega raíz a la pila
            nodes.Push(initialState);
            output = TryFindSolution(nodes);
            while ((nodes.Count > 0) && (!output))
            {
                State current;
                List<State> successors;
                // Remueve el elemento del tope
                current = nodes.Pop();
                successors = current.GetSuccessors();
                output = TryFindSolution(successors);
                // Agrega sucesores al tope de la pila (en orden inverso para expandir primero el primer sucesor)
                for (int i = successors.Count - 1; i >= 0; i--)
                    nodes.Push(successors[i]);
            }
            return (output);
        }

        public bool SearchDLS(State initialState, int maxDepth)
        {
            bool output;
            Stack<State> nodes;
            InitializeSearch();
            nodes = new Stack<State>();
            // Agrega raíz a la pila
            nodes.Push(initialState);
            output = TryFindSolution(nodes);
            while ((nodes.Count > 0) && (!output))
            {
                State current;
                // Remueve el elemento del tope
                current = nodes.Pop();
                if (current.GetDepth () < maxDepth)
                {
                    List<State> successors;
                    successors = current.GetSuccessors();
                    output = TryFindSolution(successors);
                    // Agrega sucesores al tope de la pila (en orden inverso para expandir primero el primer sucesor)
                    for (int i = successors.Count - 1; i >= 0; i--)
                        nodes.Push(successors[i]);
                }
            }
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
