using System;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;

namespace filemanager.Domain
{
    public class WinFileMoveProcess : IFileMoveProcess
    {
        private readonly IFile File;
        public bool KeepOriginal { get; set; }

        public WinFileMoveProcess(IFile file, bool keepOriginal)
        {
            File = file;
            KeepOriginal = keepOriginal;
        }

        public void To(IFile destFile)
        {
            if (File is WinFile)
            {
                if (System.IO.File.Exists(destFile.Path.Path))
                    throw new FileAlreadyExistException();

                var file = (ITextFile)destFile;

                using (var stream = System.IO.File.OpenRead(file.Path.Path))
                    file.Create(stream);

                if (!KeepOriginal)
                    System.IO.File.Delete(File.Path.Path);
            }
            else if (File is WinFolder)
            {
                if (System.IO.Directory.Exists(destFile.Path.Path))
                    throw new FileAlreadyExistException();

                var folder = (WinFolder)File;

            }
            else
                throw new ArgumentException();
        }
    }
}