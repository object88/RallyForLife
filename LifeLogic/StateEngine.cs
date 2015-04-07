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

            // Check all neighbors.  Look up, then down, then left, then right.

            // The "wrapping" was something that was explored while trying to understand
            // the example output.  In this case, if we look beyond the bounds of the array,
            // we are just looking at the other side, rather than discounting those
            // coordinates.  Note that the tests do not have any way of knowing whether
            // wrapping is set to true, and will not account for that behavior.
            const bool wrapping = false;
            if (wrapping)
            {
                // Next, count neighbors.  Check cell above.
                neighbors = WrappedCheckCell(state, row - 1, column, neighbors);
                neighbors = WrappedCheckCell(state, row + 1, column, neighbors);
                neighbors = WrappedCheckCell(state, row, column - 1, neighbors);
                neighbors = WrappedCheckCell(state, row, column + 1, neighbors);
            }
            else
            {
                neighbors = CheckCell(state, row - 1, column, neighbors);
                neighbors = CheckCell(state, row + 1, column, neighbors);
                neighbors = CheckCell(state, row, column - 1, neighbors);
                neighbors = CheckCell(state, row, column + 1, neighbors);
            }

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

        private static int CheckCell(IState state, int y, int x, int count)
        {
            if (state.ValidateLocation(y, x) && state[y, x])
            {
                count++;
            }

            return count;
        }

        private static int WrappedCheckCell(IState state, int y, int x, int count)
        {
            if (x < 0)
            {
                x = state.Width - 1;
            }
            else if (state.Width <= x)
            {
                x = 0;
            }

            if (y < 0)
            {
                y = state.Height - 1;
            }
            else if (state.Height <= y)
            {
                y = 0;
            }

            if (state[y, x])
            {
                count++;
            }

            return count;
        }
    }
}
