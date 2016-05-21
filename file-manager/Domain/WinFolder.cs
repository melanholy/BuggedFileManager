using System.Collections.Generic;
using System.IO;
using System.Linq;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class WinFolder : IFolder
    {
        public string Name { get; set; }
        public MyPath Path;

        public IEnumerable<IFile> EnumerateFiles()
        {
            var files = Directory.EnumerateFiles(Path.Path);
            var dirs = Directory.EnumerateDirectories(Path.Path);
            return files
                .Select(x => (IFile)new WinFile(new MyPath(x)))
                .Union(dirs.Select(x => new WinFolder(new MyPath(x))));
        }

        public WinFolder(MyPath path)
        {
            Path = path;
            Name = path.GetFileName();
        }

        public void Create()
        {
            if (Directory.Exists(Path.Path))
                throw new FileAlreadyExistException();
            Directory.CreateDirectory(Path.Path);
        }

        public void Delete()
        {
            Directory.Delete(Path.Path);
        }

        public static IEnumerable<WinFolder> GetRootFolders()
        {
            return DriveInfo.GetDrives()
                .Where(x => x.DriveType == DriveType.Fixed)
                .Select(x => new WinFolder(new MyPath(x.Name)));
        }
    }
}
