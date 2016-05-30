using System;
using System.IO;
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
            File = file;
            KeepOriginal = keepOriginal;
            Client = client;
        }
        
        public void To(MyPath path)
        {
            if (File is FtpFile)
            {
                if (Client.FileExists(path.Path))
                    throw new FileAlreadyExistException();

                var stream = new MemoryStream();
                Client.Download(File.Path.Path, stream);
                Client.Upload(path.Path, stream);
                if (!KeepOriginal)
                    Client.DeleteFile(File.Path.Path);
            }
            else if (File is FtpFolder)
            {
                if (Client.FolderExists(path.Path))
                    throw new FileAlreadyExistException();

                var folder = (FtpFolder) File;

            }
            else
                throw new ArgumentException();
        }
    }
}