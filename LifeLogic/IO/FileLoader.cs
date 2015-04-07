using System;
using System.IO;
using LifeLogic.Exceptions;

namespace LifeLogic.IO
{
    /// <summary>
    /// The FileLoader is a very thin veneer, obscuring the file system.  Primarily used to trap
    /// and wrap any exceptions coming from opening a file, while giving direct access to a stream
    /// reader on the file.
    /// </summary>
    public class FileLoader : IDisposable
    {
        private readonly string _path;

        public FileLoader(string path)
        {
            _path = path;
        }

        public TextReader Open()
        {
            try
            {
                Stream = new StreamReader(File.OpenRead(_path));
            }
            catch (ArgumentNullException ane)
            {
                throw new FileAccessException(_path, ane);
            }
            catch (ArgumentException ae)
            {
                throw new FileAccessException(_path, ae);
            }
            catch (PathTooLongException ptne)
            {
                throw new FileAccessException(_path, ptne);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                throw new FileAccessException(_path, dnfe);
            }
            catch (UnauthorizedAccessException uae)
            {
                throw new FileAccessException(_path, uae);
            }
            catch (FileNotFoundException fnfe)
            {
                throw new FileAccessException(_path, fnfe);
            }
            catch (NotSupportedException nse)
            {
                throw new FileAccessException(_path, nse);
            }

            return Stream;
        }

        public StreamReader Stream { get; private set; }
        
        public void Dispose()
        {
            if (null != Stream)
            {
                Stream.Close();
                Stream.Dispose();
                Stream = null;
            }
        }
    }
}
