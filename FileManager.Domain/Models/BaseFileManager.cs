using FileManager.Domain.Infrastructure;

namespace FileManager.Domain.Models
{
    public abstract class BaseFileManager
    {
        public MyPath CurrentPath { get; protected set; }
        
        public virtual void Go(Folder folder)
        {
            CurrentPath = folder.Path;
        }

        public void Delete(MyFile file)
        {
            file.Delete();
        }

        public abstract IFileMoveProcess Move(MyFile file, bool keepOriginal);
        public abstract Folder GoUp();
        public abstract void Create<TFile>(string filename) where TFile : MyFile;
        public abstract Folder Go(MyPath path);
    }
}
