using System;
using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;
using Limilabs.FTP.Client;

namespace FileManager.Domain.FTP
{
    public class FtpFileMoveProcess : IFileMoveProcess
    {
        public bool KeepOriginal { get; set; }
        private readonly MyFile File;
        private readonly Ftp Client;

        public FtpFileMoveProcess(MyFile file, bool keepOriginal, Ftp client)
        {
            if (!(file is FtpFile || file is FtpFolder))
                throw new ArgumentException();

            File = file;
            KeepOriginal = keepOriginal;
            Client = client;
        }

        private void MoveFile(TextFile destFile)
        {
            using (var stream = new MemoryStream())
            {
                Client.Download(File.Path.PathStr, stream);
                destFile.Create(stream);
            }

            if (!KeepOriginal)
                Client.DeleteFile(File.Path.PathStr);
        }
        
        public void To(MyFile destFile)
        {
            if (File is FtpFile)
            {
                if (Client.FileExists(destFile.Path.PathStr))
                    throw new FileAlreadyExistException();

                MoveFile((TextFile)destFile);
            }
            else if (File is FtpFolder)
            {
                if (Client.FolderExists(destFile.Path.PathStr))
                    throw new FileAlreadyExistException();

                var folder = (FtpFolder) File;

            }
            else
                throw new ArgumentException();
        }
    }
}