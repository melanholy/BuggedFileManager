using FileManager.Domain.Infrastructure;
using FileManager.Domain.Interfaces;

namespace FileManager.Domain.Windows
{
    public class WinFileManager : IFileManager
    {
        private readonly HistoryKeeper<Folder> History;
        public MyPath CurrentPath { get; private set; }

        public WinFileManager(Folder root)
        {
            CurrentPath = root.Path;
            History = new HistoryKeeper<Folder>(root);
        }

        public Folder GoForward()
        {
            var folder = History.GoBack();
            CurrentPath = folder.Path;
            return folder;
        }

        public Folder GoBack()
        {
            var folder = History.GoBack();
            CurrentPath = folder.Path;
            return folder;
        }

        public void Go(Folder folder)
        {
            CurrentPath = folder.Path;
            History.Do(folder);
        }

        public Folder GoUp()
        {
            CurrentPath = CurrentPath.GetDirectory();
            return new WinFolder(CurrentPath);
        }

        public void Delete(MyFile file)
        {
            file.Delete();
        }

        public void Create(string filename)
        {
            
        }
    }
}