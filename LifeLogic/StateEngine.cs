using System;

namespace LifeLogic
{
    /// <summary>
    /// The state enginer is what processes a state object, and toggles all bits to their
    /// next iteration.  As such, it has exactly one public function.
    /// Note that as this is a state engine, the object returned is the same instance as
    /// the object passed in.
    /// </summary>
    public class StateEngine 
        : IStateEngine
    {
        internal StateEngine()
        {
        }

        IState IStateEngine.Iterate(IState state)
        {
            if (null == state)
            {
                throw new ArgumentNullException("state");
            }

            bool[,] newValues = new bool[state.Height, state.Width];

            for (int y = 0; y < state.Height; y++)
            {
                for (int x = 0; x < state.Width; x++)
                {
                    newValues[y, x] = CalculateNextCellState(state, y, x);
                }
            }

            state.Reset(newValues);

            return state;
        }

        private static bool CalculateNextCellState(IState state, int row, int column)
        {
            int neighbors = 0;

            // First, check to see if this cell is live or dead.
            bool living = state[row, column];

            // Check all neighbors.  Look up, then upper-right, right, lower-right, so forth.

            neighbors += CheckCell(state, row - 1, column);
            neighbors += CheckCell(state, row - 1, column + 1);
            neighbors += CheckCell(state, row, column + 1);
            neighbors += CheckCell(state, row + 1, column + 1);
            neighbors += CheckCell(state, row + 1, column);
            neighbors += CheckCell(state, row + 1, column - 1);
            neighbors += CheckCell(state, row, column - 1);
            neighbors += CheckCell(state, row - 1, column - 1);

            if (living)
            {
                if (2 == neighbors || 3 == neighbors)
                {
                    return true;
                }
            }
            else
            {
                if (3 == neighbors)
                {
                    return true;
                }
            }

            return false;
        }

        private static int CheckCell(IState state, int y, int x)
        {
            if (state.ValidateLocation(y, x) && state[y, x])
            {
                return 1;
            }

            return 0;
        }
    }
}
