using System;
using System.Reflection;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;

namespace FileManager.Domain.Windows
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

        private void MoveFile(TextFile destFile)
        {
            using (var stream = System.IO.File.OpenRead(destFile.Path.PathStr))
                destFile.Create(stream);

            if (!KeepOriginal)
                System.IO.File.Delete(File.Path.PathStr);
        }

        public void To(MyFile destFile)
        {
            if (File is WinFile)
            {
                if (System.IO.File.Exists(destFile.Path.PathStr))
                    throw new FileAlreadyExistException();

                MoveFile((TextFile)destFile);
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