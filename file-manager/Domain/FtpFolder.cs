using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using filemanager.Domain.Interfaces;
using filemanager.Infrastructure;
using Limilabs.FTP.Client;

namespace filemanager.Domain
{
    public class FtpFolder : IFolder
    {
        public MyPath Path { get; }
        private readonly Ftp Client;

        public FtpFolder(MyPath path, Ftp client)
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
            if (Client.FolderExists(Path.Path))
                throw new FileAlreadyExistException();

            Client.CreateFolder(Path.Path);
        }

        public void Delete()
        {
            if (!Client.FolderExists(Path.Path))
                throw new FileNotFoundException();

            Client.DeleteFolderRecursively(Path.Path);
        }

        public IFileMoveProcess Move(bool keepOriginal)
        {
            if (!Client.FolderExists(Path.Path))
                throw new FileNotFoundException();

            return new FtpFileMoveProcess(this, keepOriginal, Client);
        }

        public IEnumerable<IFile> EnumerateFiles()
        {
            var list = Client.List(Path.Path);
            return list.Select(x => 
                x.IsFile 
                ? (IFile)new FtpFile(Path.Join(x.Name), Client) 
                : new FtpFolder(Path.Join(x.Name), Client)
            );
        }
    }
}
