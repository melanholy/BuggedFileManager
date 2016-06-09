using System;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models.Files;

namespace FileManager.Domain.Models.Windows
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
            if (destFile.Exists())
                throw new FileAlreadyExistException();

            if (File is WinFile)
                MoveFile((TextFile)destFile);
            else if (File is WinFolder)
            {
                var folder = (WinFolder)File;
                FolderCopier.Copy(folder, (Folder)destFile, MoveFile);
            }
            else
                throw new ArgumentException();
        }
    }
}