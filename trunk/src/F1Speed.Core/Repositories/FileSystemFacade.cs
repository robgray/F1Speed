using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace F1Speed.Core.Repositories
{
    public class FileSystemFacade : IFileSystemFacade
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public DirectoryInfo CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path);
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public void DeleteFile(string fileName)
        {
            File.Delete(fileName);
        }

        public void WriteAllLines(string fileName, IEnumerable<string> lines)
        {
            File.WriteAllLines(fileName, lines);
        }

        public Stream OpenFileStream(string fileName, FileMode fileMode)
        {
            return File.Open(fileName, fileMode);
        }
    }
}
