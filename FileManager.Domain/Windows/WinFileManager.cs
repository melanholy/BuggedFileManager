using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;

namespace FileManager.Domain.Windows
{
    public class WinFileManager : FileManagerWithHistory
    {
        public WinFileManager(Folder root)
        {
            CurrentPath = root.Path;
            History = new HistoryKeeper<Folder>(root);
        }

        public override IFileMoveProcess Move(MyFile file, bool keepOriginal)
        {
            if (!file.Exists())
                throw new FileNotFoundException();

            return new WinFileMoveProcess(file, keepOriginal);
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
                file = new WinFile(CurrentPath.Join(filename));
            else
                file = new WinFolder(CurrentPath.Join(filename));
            file.Create();
        }

        public override Folder Go(MyPath path)
        {
            var folder = new WinFolder(path);
            if (!folder.Exists())
                throw new DirectoryNotFoundException();

            CurrentPath = path;
            return folder;
        }
    }
}
