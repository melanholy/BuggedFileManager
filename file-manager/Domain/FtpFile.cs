using System;
using System.IO;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;
using Limilabs.FTP.Client;

namespace filemanager.Domain
{
    public class FtpFile : ITextFile
    {
        public MyPath Path { get; }
        private readonly Ftp Client;
        public string Extension => Path.GetExt();

        public FtpFile(MyPath path, Ftp client)
        {
            if (!client.IsMutuallyAuthenticated)
                throw new ArgumentException("Ftp-клиент не авторизован");
            if (!client.Connected)
                throw new ArgumentException("Ftp-клиент не соединен ни с каким сервером");

            Client = client;
            Path = path;
        }

        public void Create()
        {
            if (Client.FileExists(Path.Path))
                throw new FileAlreadyExistException();

            Client.Upload(Path.Path, new byte[0]);
        }

        public void Create(Stream contents)
        {
            if (Client.FileExists(Path.Path))
                throw new FileAlreadyExistException();

            Client.Upload(Path.Path, contents);
        }

        public void Delete()
        {
            if (!Client.FileExists(Path.Path))
                throw new FileNotFoundException();

            Client.DeleteFile(Path.Path);
        }

        public IFileMoveProcess Move(bool keepOriginal)
        {
            if (!Client.FileExists(Path.Path))
                throw new FileNotFoundException();

            return new FtpFileMoveProcess(this, keepOriginal, Client);
        }
    }
}
