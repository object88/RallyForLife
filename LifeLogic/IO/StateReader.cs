using System.Collections.Generic;
using System.IO;
using LifeLogic.Exceptions;
using LifeLogic.Factories;

namespace LifeLogic.IO
{
    /// <summary>
    /// The state reader will accept a stream via a TextReader, and generate a State object
    /// whose contents reflect the text.  Here we also validate for bad input, in the form of
    /// invalid characters (only accept "0" and "1") and arrays with jagged edges.
    /// </summary>
    public class StateReader
        : IStateReader
    {
        private readonly IFactory _factory;
        private readonly TextReader _streamReader;

        internal StateReader(IFactory factory, TextReader streamReader)
        {
            _factory = factory;
            _streamReader = streamReader;
        }

        public IState Read()
        {
            if (null == _streamReader)
            {
                throw new EmptyInputException();
            }

            int width = -1;
            LinkedList<string> rows = new LinkedList<string>();


            // Read in the input, line by line.  If we end up with a jagged input, throw an exception.
            // Be nice and pass over empty lines.
            while (-1 != _streamReader.Peek())
            {
                string row = _streamReader.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(row))
                {
                    continue;
                }

                if (-1 == width)
                {
                    width = row.Length;
                }
                else if (width != row.Length)
                {
                    throw new JaggedInputException();
                }

                rows.AddLast(row);
            }

            if (0 == rows.Count)
            {
                throw new EmptyInputException();
            }

            bool[,] newValues = new bool[rows.Count, width];
            int y = 0;
            foreach (string row in rows)
            {
                for (int x = 0; x < width; x++)
                {
                    char c = row[x];
                    if ('0' != c && '1' != c)
                    {
                        throw new InvalidCharacterInputException(c, y, x);
                    }

                    newValues[y, x] = '1' == c;
                }

                y++;
            }

            IState s = _factory.CreateState(rows.Count, width);
            s.Reset(newValues);

            return s;
        }
    }
}
