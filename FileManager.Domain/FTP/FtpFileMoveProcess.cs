using System;
using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Interfaces;
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
        
        public void To(MyFile destFile)
        {
            if (File is FtpFile)
            {
                var file = (TextFile)destFile;

                if (Client.FileExists(file.Path.ToString()))
                    throw new FileAlreadyExistException();

                using (var stream = new MemoryStream())
                {
                    Client.Download(File.Path.ToString(), stream);
                    file.Create(stream);
                }

                if (!KeepOriginal)
                    Client.DeleteFile(File.Path.ToString());
            }
            else if (File is FtpFolder)
            {
                if (Client.FolderExists(destFile.Path.ToString()))
                    throw new FileAlreadyExistException();

                var folder = (FtpFolder) File;

            }
            else
                throw new ArgumentException();
        }
    }
}