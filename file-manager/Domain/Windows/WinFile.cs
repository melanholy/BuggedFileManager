using System.IO;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;

namespace filemanager.Domain.Windows
{
    public class WinFile : TextFile
    {
        public WinFile(MyPath path)
        {
            var info = new System.IO.FileInfo(path.PathStr);
            Info = new FileInfo(
                new FileSize(info.Length), 
                info.CreationTime, info.LastWriteTime
            );
            Path = path;
        }
        
        public override void Create(Stream contents)
        {
            Create();

            using (var s = File.OpenWrite(Path.PathStr))
                contents.CopyTo(s);
        }

        public override void Create()
        {
            if (File.Exists(Path.PathStr))
                throw new FileAlreadyExistException();

            File.Create(Path.PathStr);
        }

        public override void Delete()
        {
            if (!File.Exists(Path.PathStr))
                throw new FileNotFoundException();

            File.Delete(Path.PathStr);
        }

        public override IFileMoveProcess Move(bool keepOriginal)
        {
            if (!File.Exists(Path.PathStr))
                throw new FileNotFoundException();

            return new WinFileMoveProcess(this, keepOriginal);
        }
    }
}
