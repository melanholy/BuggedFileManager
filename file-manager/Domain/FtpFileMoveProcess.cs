using System;
using System.IO;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;
using Limilabs.FTP.Client;

namespace filemanager.Domain
{
    public class FtpFileMoveProcess : IFileMoveProcess
    {
        public bool KeepOriginal { get; set; }
        private readonly IFile File;
        private readonly Ftp Client;

        public FtpFileMoveProcess(IFile file, bool keepOriginal, Ftp client)
        {
            if (!(file is FtpFile || file is FtpFolder))
                throw new ArgumentException();

            File = file;
            KeepOriginal = keepOriginal;
            Client = client;
        }
        
        public void To(IFile destFile)
        {
            if (File is FtpFile)
            {
                var file = (ITextFile)destFile;

                if (Client.FileExists(file.Path.Path))
                    throw new FileAlreadyExistException();

                using (var stream = new MemoryStream())
                {
                    Client.Download(File.Path.Path, stream);
                    file.Create(stream);
                }

                if (!KeepOriginal)
                    Client.DeleteFile(File.Path.Path);
            }
            else if (File is FtpFolder)
            {
                if (Client.FolderExists(destFile.Path.Path))
                    throw new FileAlreadyExistException();

                var folder = (FtpFolder) File;

            }
            else
                throw new ArgumentException();
        }
    }
}