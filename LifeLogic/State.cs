using System;
using LifeLogic.DTO;
using LifeLogic.Exceptions;

namespace LifeLogic
{
    /// <summary>
    /// A slightly smarter wrapper around the quasi-DTO, which validates arguments.
    /// </summary>
    public class State
        : IState
    {
        private readonly StateDTO _state;

        internal State(int height, int width)
        {
            Height = height;
            Width = width;

            _state = new StateDTO(height * width);
        }

        public bool this[int row, int column]
        {
            get
            {
                TestArguments(row, column);

                int index = CalculateIndex(row, column);

                return _state[index];
            }
            set
            {
                TestArguments(row, column);

                int index = CalculateIndex(row, column);

                _state[index] = value;
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public void Reset(bool[,] values)
        {
            if (values.GetLength(0) != Height)
            {
                throw new ArgumentException();
            }

            if (values.GetLength(1) != Width)
            {
                throw new ArgumentException();
            }

            int index = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _state[index++] = values[y, x];
                }
            }
        }

        public bool ValidateLocation(int row, int column)
        {
            return 0 <= row && Height > row && 0 <= column && Width > column;
        }

        private int CalculateIndex(int row, int column)
        {
            int index = (row * Width) + column;
            return index;
        }

        private void TestArguments(int row, int column)
        {
            if (false == ValidateLocation(row, column))
            {
                throw new CellAccessException(row, column);
            }
        }
    }
}
