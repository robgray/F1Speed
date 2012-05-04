using System.Collections.Generic;
using System.IO;

namespace F1Speed.Core.Repositories
{
    public interface IFileSystemFacade
    {
        bool DirectoryExists(string path);
        DirectoryInfo CreateDirectory(string path);
        bool FileExists(string fileName);
        void DeleteFile(string fileName);
        void WriteAllLines(string fileName, IEnumerable<string> lines);
        Stream OpenFileStream(string fileName, FileMode fileMode);
    }
}