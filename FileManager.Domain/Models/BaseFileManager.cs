using FileManager.Domain.Infrastructure;

namespace FileManager.Domain.Models
{
    public abstract class BaseFileManager
    {
        protected HistoryKeeper<Folder> History;
        public MyPath CurrentPath { get; protected set; }

        public Folder GoForward()
        {
            var folder = History.GoForward();
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

        public void Delete(MyFile file)
        {
            file.Delete();
        }

        public abstract Folder GoUp();
        public abstract void Create<TFile>(string filename) where TFile : MyFile;
    }
}
