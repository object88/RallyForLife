using System.Globalization;

namespace LifeLogic.Exceptions
{
    public class InvalidCharacterInputException
        : ParseException
    {
        private char _c;
        private int _x, _y;

        public InvalidCharacterInputException(char c, int row, int column)
        {
            _c = c;
            _x = column;
            _y = row;
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.CurrentUICulture, ErrorStrings.InvalidCharacter, _x, _y, _c);
            }
        }
    }
}
