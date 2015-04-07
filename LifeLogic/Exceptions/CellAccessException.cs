using System;
using System.Globalization;

namespace LifeLogic.Exceptions
{
    /// <summary>
    /// Thrown when the program attemps to access a cell with at a bad set of coordinates,
    /// either negative or beyond the reach of the width or height.
    /// </summary>
    public class CellAccessException
        : Exception
    {
        private readonly int _x, _y;

        public CellAccessException(int y, int x)
        {
            _x = x;
            _y = y;
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.CurrentUICulture, ErrorStrings.IllegalCell, _x, _y);
            }
        }
    }
}
