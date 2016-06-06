using filemanager.Infrastructure;
using System.IO;
using filemanager.Domain.Interfaces;

namespace filemanager.Domain
{
    public class WinFile : ITextFile
    {
        public MyPath Path { get; }
        public string Extension => Path.GetExt();

        public WinFile(MyPath path)
        {
            Path = path;
        }
        
        public void Create(Stream contents)
        {
            Create();

            using (var s = File.OpenWrite(Path.Path))
                contents.CopyTo(s);
        }

        public void Create()
        {
            if (File.Exists(Path.Path))
                throw new FileAlreadyExistException();

            File.Create(Path.Path);
        }

        public void Delete()
        {
            if (!File.Exists(Path.Path))
                throw new FileNotFoundException();

            File.Delete(Path.Path);
        }

        public IFileMoveProcess Move(bool keepOriginal)
        {
            if (!File.Exists(Path.Path))
                throw new FileNotFoundException();

            return new WinFileMoveProcess(this, keepOriginal);
        }
    }
}
