using System.Collections.Generic;
using System.IO;
using System.Linq;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class WinFolder : IFolder
    {
        public MyPath Path { get; }

        public WinFolder(MyPath path)
        {
            Path = path;
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            var files = Directory.EnumerateFiles(Path.Path);
            var dirs = Directory.EnumerateDirectories(Path.Path);
            return files
                .Select(x => (IFile)new WinFile(new MyPath(x)))
                .Union(dirs.Select(x => new WinFolder(new MyPath(x))));
        }

        public void Create()
        {
            if (Directory.Exists(Path.Path))
                throw new FileAlreadyExistException();

            Directory.CreateDirectory(Path.Path);
        }

        public void Delete()
        {
            if (!Directory.Exists(Path.Path))
                throw new FileNotFoundException();

            Directory.Delete(Path.Path, true);
        }

        public IFileMoveProcess Move(bool keepOriginal)
        {
            if (!Directory.Exists(Path.Path))
                throw new FileNotFoundException();

            return new WinFileMoveProcess(this, keepOriginal);
        }

        public static IEnumerable<WinFolder> GetRootFolders()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed)
                .Select(x => new WinFolder(new MyPath(x.Name)));
        }
    }
}
