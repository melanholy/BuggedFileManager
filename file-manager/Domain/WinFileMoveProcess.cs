using System;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class WinFileMoveProcess : IFileMoveProcess
    {
        private readonly MyFile File;
        public bool KeepOriginal { get; set; }

        public WinFileMoveProcess(MyFile file, bool keepOriginal)
        {
            File = file;
            KeepOriginal = keepOriginal;
        }

        public void To(MyFile destFile)
        {
            if (File is WinFile)
            {
                if (System.IO.File.Exists(destFile.Path.PathStr))
                    throw new FileAlreadyExistException();

                var file = (TextMyFile)destFile;
                using (var stream = System.IO.File.OpenRead(file.Path.PathStr))
                    file.Create(stream);

                if (!KeepOriginal)
                    System.IO.File.Delete(File.Path.PathStr);
            }
            else if (File is WinFolder)
            {
                if (System.IO.Directory.Exists(destFile.Path.PathStr))
                    throw new FileAlreadyExistException();

                var folder = (WinFolder)File;

            }
            else
                throw new ArgumentException();
        }
    }
}