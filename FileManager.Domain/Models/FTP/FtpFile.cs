using System;
using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models.Files;
using Limilabs.FTP.Client;
using FileInfo = FileManager.Domain.Models.Files.FileInfo;

namespace FileManager.Domain.Models.FTP
{
    public class FtpFile : TextFile
    {
        private readonly Ftp Client;

        public FtpFile(MyPath path, Ftp client)
        {
            if (!client.Connected)
                throw new ArgumentException("Ftp-клиент не соединен ни с каким сервером");
            
            Client = client;
            Path = path;
            Info = GetFileInfo();
        }

        public FtpFile(MyPath path, Ftp client, FileInfo info) : this(path, client)
        {
            Info = info;
        }

        private FileInfo GetFileInfo()
        {
            var size = Client.GetFileSize(Path.PathStr);
            var time = Client.GetFileModificationTime(Path.PathStr);
            return new FileInfo(new FileSize(size), time, time);
        }

        public override void Create()
        {
            if (Exists())
                throw new FileAlreadyExistException();

            Client.Upload(Path.PathStr, new byte[0]);
        }

        public override void Create(Stream contents)
        {
            if (Exists())
                throw new FileAlreadyExistException();

            Client.Upload(Path.PathStr, contents);
        }

        public override void Delete()
        {
            if (!Exists())
                throw new FileNotFoundException();

            Client.DeleteFile(Path.PathStr);
        }

        public override bool Exists()
        {
            return Client.FileExists(Path.PathStr);
        }
    }
}
