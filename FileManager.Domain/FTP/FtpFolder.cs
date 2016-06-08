using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Interfaces;
using Limilabs.FTP.Client;

namespace FileManager.Domain.FTP
{
    public class FtpFolder : Folder
    {
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

        public FtpFolder(MyPath path, Ftp client, FileInfo info) : this(path, client)
        {
            Info = info;
        }

        public override void Create()
        {
            if (Client.FolderExists(Path.PathStr))
                throw new FileAlreadyExistException();

            Client.CreateFolder(Path.PathStr);
        }

        public override void Delete()
        {
            if (!Client.FolderExists(Path.PathStr))
                throw new FileNotFoundException();

            Client.DeleteFolderRecursively(Path.PathStr);
        }

        public override IFileMoveProcess Move(bool keepOriginal)
        {
            if (!Client.FolderExists(Path.PathStr))
                throw new FileNotFoundException();

            return new FtpFileMoveProcess(this, keepOriginal, Client);
        }

        public override IEnumerable<MyFile> EnumerateFiles()
        {
            var list = Client.List(Path.PathStr);
            return list.Select(x => 
                x.IsFile 
                ? (MyFile)new FtpFile(
                      Path.Join(x.Name), Client, 
                      new FileInfo(new FileSize(x.Size.Value), x.ModifyDate, x.ModifyDate)
                  )
                : new FtpFolder(
                      Path.Join(x.Name), Client, 
                      new FileInfo(new FileSize(FileSize.DirSize), x.ModifyDate, x.ModifyDate)
                  )
            );
        }
    }
}
