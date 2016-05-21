using filemanager.Infrastructure;

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
            if (System.IO.File.Exists(Path.Path))
                throw new FileAlreadyExistException();
            System.IO.File.Create(Path.Path);
        }

        public void Delete()
        {
            System.IO.File.Delete(Path.Path);
        }
    }
}
