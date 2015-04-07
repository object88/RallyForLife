using System;
using System.Globalization;

namespace LifeLogic.Exceptions
{
    /// <summary>
    /// Thrown when there is an issue accessing the file provided as input to the application.
    /// </summary>
    public class FileAccessException
        : Exception
    {
        private readonly string _path;

        public FileAccessException(string path, Exception innerException)
            : base("", innerException)
        {
            _path = path;
        }

        public override string Message
        {
            get
            {
                return string.Format(CultureInfo.CurrentUICulture, ErrorStrings.InvalidFile, _path);
            }
        }
    }
}
