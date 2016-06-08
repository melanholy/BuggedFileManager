using System;
using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Interfaces;
using Limilabs.FTP.Client;

namespace FileManager.Domain.FTP
{
    public class FtpFile : TextFile
    {
        private readonly Ftp Client;

        public FtpFile(MyPath path, Ftp client)
        {
            if (!client.IsMutuallyAuthenticated)
                throw new ArgumentException("Ftp-клиент не авторизован");
            if (!client.Connected)
                throw new ArgumentException("Ftp-клиент не соединен ни с каким сервером");
            
            
            Client = client;
            Path = path;
        }

        public FtpFile(MyPath path, Ftp client, FileInfo info) : this(path, client)
        {
            Info = info;
        }

        public override void Create()
        {
            if (Client.FileExists(Path.PathStr))
                throw new FileAlreadyExistException();

            Client.Upload(Path.PathStr, new byte[0]);
        }

        public override void Create(Stream contents)
        {
            if (Client.FileExists(Path.PathStr))
                throw new FileAlreadyExistException();

            Client.Upload(Path.PathStr, contents);
        }

        public override void Delete()
        {
            if (!Client.FileExists(Path.PathStr))
                throw new FileNotFoundException();

            Client.DeleteFile(Path.PathStr);
        }

        public override IFileMoveProcess Move(bool keepOriginal)
        {
            if (!Client.FileExists(Path.PathStr))
                throw new FileNotFoundException();

            return new FtpFileMoveProcess(this, keepOriginal, Client);
        }
    }
}
