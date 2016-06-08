using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models;

namespace FileManager.Domain.Windows
{
    public class WinFileManager : BaseFileManager
    {
        public WinFileManager(Folder root)
        {
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
