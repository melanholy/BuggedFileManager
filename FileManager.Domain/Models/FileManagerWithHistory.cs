using FileManager.Domain.Infrastructure;

namespace FileManager.Domain.Models
{
    public abstract class FileManagerWithHistory : BaseFileManager
    {
        protected HistoryKeeper<Folder> History;

        public Folder GoForward()
        {
            var folder = History.GoForward();
            CurrentPath = folder.Path;
            return folder;
        }

        public override void Go(Folder folder)
        {
            CurrentPath = folder.Path;
            History.Do(folder);
        }

        public Folder GoBack()
        {
            var folder = History.GoBack();
            CurrentPath = folder.Path;
            return folder;
        }
    }
}
