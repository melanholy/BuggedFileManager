using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;
using FileManager.Domain.Windows;
using Limilabs.FTP.Client;

namespace FileManager.Domain.FTP
{
    public class FtpFileManager : FileManagerWithHistory
    {
        private readonly Ftp Client;

        public FtpFileManager(Folder root, Ftp client)
        {
            Client = client;
            CurrentPath = root.Path;
            History = new HistoryKeeper<Folder>(root);
        }
        
        public override Folder GoUp()
        {
            CurrentPath = CurrentPath.GetDirectory();
            return new WinFolder(CurrentPath);
        }

        public override void Create<TFile>(string filename)
        {
            MyFile file;
            if (typeof(TFile).IsSubclassOf(typeof(TextFile)))
                file = new FtpFile(CurrentPath.Join(filename), Client);
            else
                file = new FtpFolder(CurrentPath.Join(filename), Client);
            file.Create();
        }

        public override IFileMoveProcess Move(MyFile file, bool keepOriginal)
        {
            if (!file.Exists())
                throw new FileNotFoundException();

            return new FtpFileMoveProcess(file, keepOriginal, Client);
        }

        public override Folder Go(MyPath path)
        {
            var folder = new FtpFolder(path, Client);
            if (!folder.Exists())
                throw new DirectoryNotFoundException();

            CurrentPath = path;
            return folder;
        }
    }
}
