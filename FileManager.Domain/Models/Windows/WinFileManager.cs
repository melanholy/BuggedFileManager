using System.IO;
using FileManager.Domain.Infrastructure;
using FileManager.Domain.Models.Files;

namespace FileManager.Domain.Models.Windows
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
            return Go(CurrentPath.GetDirectory());
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

            Go(folder);
            return folder;
        }
    }
}
