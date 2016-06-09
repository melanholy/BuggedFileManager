using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;
using Limilabs.FTP.Client;

namespace FileManager.Domain.FTP
{
    public class FtpFolder : Folder
    {
        private readonly Ftp Client;

        public FtpFolder(MyPath path, Ftp client)
        {
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
            if (Exists())
                throw new FileAlreadyExistException();

            Client.CreateFolder(Path.PathStr);
        }

        public override void Delete()
        {
            if (!Exists())
                throw new FileNotFoundException();

            Client.DeleteFolderRecursively(Path.PathStr);
        }

        public override bool Exists()
        {
            return Client.FolderExists(Path.PathStr);
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
