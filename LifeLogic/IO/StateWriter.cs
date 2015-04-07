using System;
using System.IO;

namespace LifeLogic.IO
{
    /// <summary>
    /// The state writer dumps the contents of the state to a stream, in the same
    /// shape as they were input.
    /// </summary>
    public class StateWriter
        : IStateWriter
    {
        private readonly TextWriter _textWriter;

        internal StateWriter(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void Write(IState state)
        {
            if (null == state)
            {
                throw new ArgumentNullException("state");
            }

            for (int y = 0; y < state.Height; y++)
            {
                for (int x = 0; x < state.Width; x++)
                {
                    _textWriter.Write(state[y, x] ? '1' : '0');
                }
                _textWriter.WriteLine();
            }
        }
    }
}
