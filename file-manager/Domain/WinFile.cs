using filemanager.Infrastructure;
using System.IO;

namespace filemanager.Domain
{
    public class WinFile : ITextFile
    {
        public MyPath Path;
        public string Name
        {
            get { return Path.GetFileName(); }
            set { }
        }
        public string Extension => 
            Path.GetExt();

        public WinFile(MyPath path)
        {
            Path = path;
        }

        public void Create()
        {
            if (File.Exists(Path.Path))
                throw new FileAlreadyExistException();
            File.Create(Path.Path);
        }

        public void Delete()
        {
            File.Delete(Path.Path);
        }
    }
}
